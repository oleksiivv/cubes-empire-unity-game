using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{

    public string gameId="4059257";
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId,false);
        
    }

    public bool showVideo(){
        if(Advertisement.IsReady("video")){
            Advertisement.Show("video");

            return true;
        }

        return false;
    }

    public void ShowRewardedVideo(){ 
     ShowOptions options = new ShowOptions();
     options.resultCallback = AdCallbackHandler;
     if (Advertisement.IsReady("rewardedVideo"))
     {
         Advertisement.Show ("rewardedVideo",options);
     }
    }

    void AdCallbackHandler(ShowResult result){
        switch (result){
        case ShowResult.Finished:
            Debug.Log ("Ad Finished. Rewarding player...");
            PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+10);
            break;
        case ShowResult.Skipped:
            Debug.Log ("Ad Skipped");
            break;
        case ShowResult.Failed:
            Debug.Log("Ad failed");
            break;
        }
    }

    
}
