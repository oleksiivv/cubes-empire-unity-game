using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsCount;

  // Start is called before the first frame update
  void Start()
  {
      fpsCount.gameObject.SetActive(false);
      
      Application.targetFrameRate = 60;


      if(PlayerPrefs.GetInt("quality",-1)!=-1){
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
            //quality.GetComponent<Dropdown>().value=PlayerPrefs.GetInt("quality");
        }
  }



  // Update is called once per frame
  void Update()
  {
      //fpsCount.text = "FPS: "+((int)(1f / Time.unscaledDeltaTime)).ToString();
  }
}
