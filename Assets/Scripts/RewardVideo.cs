using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideo : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
    bool reward = false;

    public GameObject RewardCanvas;
    public Text RewardText;
    public Text RewardAmount;
    public Image RewardSprite;

    //This is our custom class with our variables
    [System.Serializable]
    public class RewardItem
    {
        public String item;
        public String text;
        public Sprite sprite;
        public int min;
        public int max;
    }

    public List<RewardItem> rewards = new List<RewardItem>(1);

    void Start()
    {
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdRewarded += HandleReward;

        this.RequestRewardBasedVideo();
    }

    void Update()
    {
        if (reward)
        {
            RewardItem r = rewards[UnityEngine.Random.Range(0, rewards.Count)];
            int amount = UnityEngine.Random.Range(r.min, r.max);
            RewardText.text = r.text;
            RewardAmount.text = amount.ToString();
            RewardSprite.sprite = r.sprite;
            RewardCanvas.SetActive(true);

            switch (r.item)
            {
                case "laser":
                    PlayerPrefs.SetInt("boosters_laser", PlayerPrefs.GetInt("boosters_laser") + amount);
                    break;

                case "shield":
                    PlayerPrefs.SetInt("boosters_shield", PlayerPrefs.GetInt("boosters_shield") + amount);
                    break;
            }

            this.RequestRewardBasedVideo();
            reward = false;
        }
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
    
    public void Show()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            Debug.Log("Couldn't load video.");
        }
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
}
