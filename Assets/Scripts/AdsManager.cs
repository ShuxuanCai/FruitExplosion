using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    public string adID = "4275959";

    Action rewardAdCompleted;

    public bool isWatchRewardAds = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Advertisement.Initialize(adID);
            Advertisement.AddListener(this);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayInterstitial()
    {
        // Play a full screen skippable ad
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
        else
        {
            Debug.Log("Interstitial not ready!");
        }
    }

    public void PlayRewardAd()
    {
        // Play an ad that rewards player afer watching video.
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
            isWatchRewardAds = true;
        }
        else
        {
            Debug.Log("Rewarded ad not ready!");
            isWatchRewardAds = false;
        }
    }

    public bool IsPlayRewardAd()
    {
        if (isWatchRewardAds == true)
        {
            return true;
        }
            
        return false;
    }

    public void PlayBannerAd()
    {
        // Show the banner ad in the game.
        if (Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_Android");
        }
        else
        {
            Debug.Log("Banner ad not ready!");
            StartCoroutine(ReloadBannerAd());
        }
    }

    IEnumerator ReloadBannerAd()
    {
        yield return new WaitForSeconds(1.0f);
        PlayBannerAd();
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new NotImplementedException();
        Debug.Log("Unity ads had an error: " + message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //throw new NotImplementedException();
        Debug.Log(placementId + " ad finished playing");
        if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            Debug.Log("Player watched reward ad! Give them rewards!");
            rewardAdCompleted.Invoke();
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new NotImplementedException();
        Debug.Log(placementId + " ad started playing!");
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new NotImplementedException();
        Debug.Log(placementId + " ad ready!");
    }
}
