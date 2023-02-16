using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject buttonMuted, buttonNormal,buttonNormalMusic,buttonMutedMusic;

    public GameObject audioController;

    public Dropdown quality;
    void Start()
    {
        //PlayerPrefs.SetInt("hi",6000);
        if(PlayerPrefs.GetInt("quality",-1)==-1){
            quality.GetComponent<Dropdown>().value=QualitySettings.GetQualityLevel();
        }
        else {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
            quality.GetComponent<Dropdown>().value=PlayerPrefs.GetInt("quality");
        }


        if(PlayerPrefs.GetInt("!sound")==0){

            buttonMuted.SetActive(false);
            buttonNormal.SetActive(true);
            //audioController.GetComponent<AudioSource>().enabled=true;

        }
        else{
            buttonMuted.SetActive(true);
            buttonNormal.SetActive(false);
            //audioController.GetComponent<AudioSource>().enabled=false;
        }


        if(PlayerPrefs.GetInt("!music")==0){

            buttonMutedMusic.SetActive(false);
            buttonNormalMusic.SetActive(true);
            //audioController.GetComponent<AudioSource>().enabled=true;

        }
        else{
            buttonMutedMusic.SetActive(!false);
            buttonNormalMusic.SetActive(!true);

            //audioController.GetComponent<AudioSource>().enabled=true;
        }
        
    }

    public void muteSound(){
        PlayerPrefs.SetInt("!sound",1);
        buttonMuted.SetActive(true);
        buttonNormal.SetActive(false);
        //audioController.GetComponent<AudioSource>().enabled=false;
        
    }

    public void unmuteSound(){
        PlayerPrefs.SetInt("!sound",0);
        buttonMuted.SetActive(false);
        buttonNormal.SetActive(true);

        //audioController.GetComponent<AudioSource>().enabled=true;

        

    }


    public void muteMusic(){
        PlayerPrefs.SetInt("!music",1);
        buttonMutedMusic.SetActive(true);
        buttonNormalMusic.SetActive(false);
        audioController.GetComponent<AudioSource>().enabled=false;
        
    }

    public void unmuteMusic(){
        PlayerPrefs.SetInt("!music",0);
        buttonMutedMusic.SetActive(false);
        buttonNormalMusic.SetActive(true);

        audioController.GetComponent<AudioSource>().enabled=true;

        

    }

    public void SetQuality(int qualityIndex){
        PlayerPrefs.SetInt("quality",qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }



}
