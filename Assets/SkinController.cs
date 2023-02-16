using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public Material[] mat;
    public GameObject player;

    public GameObject[] studyPanels;
    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {

        player.gameObject.GetComponent<MeshRenderer>().material=mat[PlayerPrefs.GetInt("current")];

        if(PlayerPrefs.GetInt("first")==0){
            bg.SetActive(true);
            studyPanels[0].SetActive(true);
            PlayerPrefs.SetInt("first",1);
        }

        
        
    }

    public void changeSkin(){
        
        player.gameObject.GetComponent<MeshRenderer>().material=mat[PlayerPrefs.GetInt("current")];

    }


    public void howTo(){
        bg.SetActive(true);
        studyPanels[0].SetActive(true);
        PlayerPrefs.SetInt("first",1);
    }
    
    public void next(int id){
        close();
        studyPanels[id].SetActive(true);
        bg.SetActive(true);
    }

    public void close(){
        bg.SetActive(false);
        foreach(var panel in studyPanels)panel.SetActive(false);
    }
}
