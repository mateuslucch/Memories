using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{

#if UNITY_ANDROID
    private string gameId = "4662353"; // gameid for android
    private string showTag = "rewardBanner";
#elif UNITY_IOS
    private string gameId = "4662352"; // gameid for ios
    private string showTag = "rewardBanner";
#endif

    public bool testMode = true;

    IEnumerator Start()
    {
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize(gameId, testMode);
        print("teste2");
        while (!Advertisement.IsReady(showTag))
        {
            yield return null;
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(showTag);
    }
}