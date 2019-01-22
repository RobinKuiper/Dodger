using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class ApplicationManager : MonoBehaviour {
    public Ships.Ship ship;
    public List<int> boosters = new List<int>();
    public int deathsBeforeAd = 3;

    public static ApplicationManager instance;

    public int continues = 0;

    void Awake() {
        instance = this;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ApplicationManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-1728933936385442~2340620904";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
    }

    void Update()
    {
        continues = PlayerPrefs.HasKey("continues") ? PlayerPrefs.GetInt("continues") : continues;
    }
}
