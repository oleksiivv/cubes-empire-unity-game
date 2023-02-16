using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsController : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] clip;
    void Start()
    {
        if(PlayerPrefs.GetInt("!sound")==0){
            src.enabled=true;

        }
        else{
            src.enabled=false;
        }
        
    }


    public void playFirst(){
        src.clip=clip[1];
        if(PlayerPrefs.GetInt("!sound")==0)src.Play();
    }

    public void playSecond(){
        src.clip=clip[0];
        if(PlayerPrefs.GetInt("!sound")==0)src.Play();
    }


    public void playDeath(){
        src.clip=clip[2];
        if(PlayerPrefs.GetInt("!sound")==0)src.Play();
    }

    
}
