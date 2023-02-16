using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource src;

    void Start()
    {
        if(PlayerPrefs.GetInt("!music")==0){
            src.enabled=true;

        }
        else{
            src.enabled=false;
        }
        
    }
}
