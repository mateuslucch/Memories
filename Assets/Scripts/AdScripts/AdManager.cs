using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;

    public static AdManager Instance;

#if UNITY_ANDROID
    private string gameId = "4662353"; // gameid for android
    //private string showTag = "Rewarded Android";
    private string showTag = "rewardVideo";
#elif UNITY_IOS
    private string gameId = "4662352"; // gameid for ios
    private string showTag = "Rewarded iOS";
#endif

    private AdTrigger adTrigger;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
    }

    public void ShowAd(AdTrigger adTrigger)
    {
        this.adTrigger = adTrigger;

        Advertisement.Show("rewardVideo");
    }


    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                // continue game
                adTrigger.ContinueGame();
                break;
            case ShowResult.Skipped:
                // Ad was skipped
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("Ads Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ads Ready");
    }
}
