using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
    private RewardBasedVideoAd continueRewardVideo;
    public InterstitialAd interstitial;

    public GameObject noVideoMessagePanel;

    bool reward = false;

    public GameObject RewardCanvas;
    public Text RewardText;
    public Text RewardAmount;
    public Image RewardSprite;

    private int rewardId = -1;

    public bool usedFreeContinue = false;

    [System.Serializable]
    public class RewardItem
    {
        public String item;
        public String text;
        public Sprite sprite;
        public int min;
        public int max;
        public bool showPanel = true;
        public int weight;
    }

    public List<RewardItem> rewards = new List<RewardItem>(1);
    public List<RewardItem> weightedRewards = new List<RewardItem>(1);

    void Start()
    {
         // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdRewarded += HandleReward;

        RequestRewardBasedVideo();
        RequestInterstitial();

        rewards.ForEach(r =>
        {
            for(int i = 0; i < r.weight; i++)
            {
                weightedRewards.Add(r);
            }
        });
    }

    void Update()
    {
        if (reward)
        {
            reward = false;
            RewardItem r = (rewardId >= 0) ? rewards[rewardId] : weightedRewards[UnityEngine.Random.Range(0, weightedRewards.Count)];
            int amount = UnityEngine.Random.Range(r.min, r.max);
            if (r.showPanel && !GameManager.instance)
            {
                RewardText.text = r.text;
                RewardAmount.text = amount.ToString();
                RewardSprite.sprite = r.sprite;
                RewardCanvas.SetActive(true);
            }

            switch (r.item)
            {
                case "laser":
                    PlayerPrefs.SetInt("boosters_laser", PlayerPrefs.GetInt("boosters_laser") + amount);
                    break;

                case "shield":
                    PlayerPrefs.SetInt("boosters_shield", PlayerPrefs.GetInt("boosters_shield") + amount);
                    break;

                case "continue":
                    if (GameManager.instance)
                    {
                        GameManager.instance.moveOn = true;
                    }
                    else
                    {
                        PlayerPrefs.SetInt("continues", PlayerPrefs.GetInt("continues") + amount);
                    }
                    break;

                case "gold":
                    Bank.instance.add(amount);
                    break;
            }

            this.RequestRewardBasedVideo();
        }
    }

    private void RequestInterstitial()
    {
        // ca-app-pub-1728933936385442/4304315550
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //TEST
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        //  ca-app-pub-1728933936385442/5670503589
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //TEST
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public bool Show(String type = "interstitial")
    {
        rewardId = -1;

        switch (type)
        {
            case "interstitial":
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    return true;
                }
                else
                {
                    Debug.Log("Couldn't load interstitial video.");
                    StartCoroutine(ShowNoVideoMessage());
                    return false;
                }
                break;

            case "reward":
                if (rewardBasedVideo.IsLoaded())
                {
                    rewardBasedVideo.Show();
                    return true;
                }
                else
                {
                    Debug.Log("Couldn't load reward video.");
                    StartCoroutine(ShowNoVideoMessage());
                    return false;
                }
                break;

            case "continue":
                rewardId = 2;

                if (rewardBasedVideo.IsLoaded())
                {
                    rewardBasedVideo.Show();
                    usedFreeContinue = true;
                    return true;
                }
                else
                {
                    Debug.Log("Couldn't load reward video.");
                    StartCoroutine(ShowNoVideoMessage());
                    return false;
                }
                break;
        }
        return false;
    }

    // Load new ad.
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {

    }

    public void HandleReward(object sender, Reward args)
    {
        reward = true;
    }

    public void Close()
    {
        RewardCanvas.SetActive(false);
    }

    IEnumerator ShowNoVideoMessage()
    {
        noVideoMessagePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        noVideoMessagePanel.SetActive(false);
    }

    public static AdManager instance;

    void Awake()
    {
        instance = this;
    }
}
