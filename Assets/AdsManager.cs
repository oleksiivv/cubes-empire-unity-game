using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(InterstitialVideo))]
public class AdsManager : RewardedVideo
{
#if UNITY_IOS
    public string gameId="4059257";
#else
    public string gameId="4059256";
#endif

    private InterstitialVideo unityAdsInterstitial;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId,false);
        
        unityAdsInterstitial = GetComponent<InterstitialVideo>();
        unityAdsInterstitial.LoadAd();

        this.LoadAd();
    }   

    public void showVideo(){
        unityAdsInterstitial.ShowAd();
    }

    public void ShowRewardedVideo(){ 
        this.ShowAd();
    }

    public override void Success(){
        PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+10);
    }
}
