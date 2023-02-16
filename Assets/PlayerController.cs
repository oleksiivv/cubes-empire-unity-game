using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public int force=250;
    public List<GameObject> childCubes=new List<GameObject>();

    public bool alive=true;

    public CubesToAdd cubesController;

    private bool jump=true;

    public GameObject camera;

    public ParticleSystem moveUpEffect;
    public ParticleSystem dieEffect;

    public GameObject ground;

    public bool cantDie;

    public GameObject startPanel;
    public GameObject runPanel;
    public GameObject pausePanel;
    public GameObject diePanel;

    public static bool restartGame=false;

    public int score=0;
    public Text[] scoreTxt;
    public Text[] bestTxt;
    public Text[] money;

    public Text[] currentCubesScore;

    private int maxCubeCount;

    public GameObject audioController;

    public AudioEffectsController audio;

    private AlivePlane alivePlane;

    public AdsManager ads;

    public AdmobController admob;
    
    // Start is called before the first frame update
    void Start()
    {
        alivePlane=camera.GetComponentInChildren<AlivePlane>();
        maxCubeCount=3;
        score=0;
        foreach(Text txt in bestTxt){
            txt.text="Best: "+PlayerPrefs.GetInt("best").ToString();
        }
        if(restartGame){

            startPanel.SetActive(false);
            runPanel.SetActive(true);
            rb=GetComponent<Rigidbody>();
            cubesController.startGame();


        }
        else{
            startPanel.SetActive(true);
            startPanel.GetComponent<Animator>().SetBool("close",false);
            startPanel.GetComponent<Animator>().SetBool("open",true);
        }

        cantDie=false;

        updateScore(0);


        if(PlayerPrefs.GetInt("!music")==0){

            audioController.GetComponent<AudioSource>().enabled=true;

        }
        else{
 
            audioController.GetComponent<AudioSource>().enabled=false;
        }

        foreach(Text m in money)m.text = PlayerPrefs.GetInt("money").ToString();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonUp(0) && jump){
        //     rb.AddForce(Vector3.up*force);
        // }
    }


    public void makeMovement(){
        if(jump){
             rb.AddForce(Vector3.up*force);
        }
    }
    void OnTriggerEnter(Collider other){

        if(other.gameObject.tag=="newcube" && alive){
            alivePlane.Exit();
            alivePlane.cubesInside=false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic=true;
            childCubes.Add(other.gameObject);
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.transform.parent=gameObject.transform;

            if(childCubes.Count%maxCubeCount==0 && childCubes.Count!=0){
                moveUp();
            }

            updateScore(1);
            audio.playFirst();

            foreach(var txt in currentCubesScore){
                txt.text=childCubes.Count.ToString()+"/"+maxCubeCount.ToString();
            }

            if(Random.Range(0,3)==1){
                int nOfCoins=Random.Range(5,10);
                coinsMessage.gameObject.SetActive(true);
                coinsMessage.text="+"+nOfCoins.ToString();
                PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+nOfCoins);
                foreach(Text m in money)m.text = PlayerPrefs.GetInt("money").ToString();

                Invoke("cleanCoinsMessage",3);
                audio.playSecond();
            }
        }

    }


    void OnTriggerExit(Collider other){
        if(other.gameObject.tag=="AlivePlane"){
            Debug.Log("die from exit alive box");
            restart();
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="ground"){
            jump=true;
            if(!alive && !cantDie){
                
                if(childCubes.Count>0){
                    Invoke(nameof(restart),0.5f);
                }
            }

            else if(transform.position.y<0.5f &&childCubes.Count>0 && !cantDie){
                
                dieEffect.Play();
                Invoke(nameof(restart),0.5f);
                

            }
        }
        if(other.gameObject.tag=="newcube" && alive){
            if(other.gameObject.transform.parent!=transform && !cantDie){

                alive=false;

                // foreach(var obj in childCubes){
                //     obj.AddComponent<Rigidbody>();
                //     obj.GetComponent<Rigidbody>().useGravity=true;
                //     obj.GetComponent<Rigidbody>().isKinematic=false;
                //     obj.GetComponent<Rigidbody>().AddForce(Vector3.up*force/3);
                //     obj.GetComponent<Rigidbody>().AddTorque(Vector3.forward*force/5);
                // }

                
                rb.AddForce(Vector3.up*force/3);
                rb.AddTorque(Vector3.forward*force/5);

                dieEffect.Play();
                if(childCubes.Count>0)Invoke(nameof(restart),0.5f);
                else Invoke(nameof(restart),0.5f);

                

            }
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="ground"){
            jump=false;
        }
    }
    
    bool played=false;
    void restart(){
        if(!played){
            audio.playDeath();
            played=true;
        }
        alive=false;
        runPanel.GetComponent<Animator>().SetBool("close",true);
        Invoke("closeRunPanel",1f);
        

    }
    
    public Text scoreMessage;
    public Text coinsMessage;
    void moveUp(){
        alivePlane.Exit();
        updateScore(maxCubeCount*maxCubeCount);

        audio.playSecond();

        scoreMessage.gameObject.SetActive(true);
        scoreMessage.text="+"+(maxCubeCount*maxCubeCount).ToString()+" points";
        Invoke("cleanMessage",3f);
        
        rb.isKinematic=true;
        rb.AddTorque(Vector3.right*10);
        cantDie=true;
        moveUpEffect.Play();
        

        camera.transform.position+=new Vector3(0,gameObject.transform.localScale.y*maxCubeCount,0);
        
        cubesController.play=false;

        foreach(var obj in childCubes){
             Destroy(obj);
            
         }
         childCubes.Clear();
        

         

         
         StartCoroutine(groundMoveUp());
    }


    void cleanMessage(){
        scoreMessage.gameObject.SetActive(false);
    }

    void cleanCoinsMessage(){
        coinsMessage.gameObject.SetActive(false);
    }

    IEnumerator groundMoveUp(){
        float groundDy=0;
        while(groundDy<gameObject.transform.localScale.y*maxCubeCount){
            groundDy+=0.1f;
            ground.transform.position+=new Vector3(0,0.1f,0);
            yield return new WaitForSeconds(0.001f);
        }

        rb.isKinematic=false;

        cantDie=false;
        
        cubesController.play=true;
        //cubesController.startGame();

        maxCubeCount++;
        foreach(var txt in currentCubesScore){
            txt.text=childCubes.Count.ToString()+"/"+maxCubeCount.ToString();
        }

        cubesController.posDy=ground.transform.position.y+0f;

        

    }


    public void pause(){
        Time.timeScale=0;
        runPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void start(){

        rb=GetComponent<Rigidbody>();
        cubesController.startGame();

        Time.timeScale=1;
        //startPanel.SetActive(false);
        
        startPanel.GetComponent<Animator>().SetBool("open",false);
        startPanel.GetComponent<Animator>().SetBool("close",true);
        settingsPanel.GetComponent<Animator>().SetBool("close",true);
        Invoke("closeSettings",1f);
        Invoke("closeMenu",1f);
        
    }

    public void backFromSettings(){
        settingsPanel.SetActive(true);
        startPanel.SetActive(true);
        startPanel.GetComponent<Animator>().SetBool("close",false);
        startPanel.GetComponent<Animator>().SetBool("open",true);

        settingsPanel.GetComponent<Animator>().SetBool("open",false);
        settingsPanel.GetComponent<Animator>().SetBool("close",true);
        
        Invoke("closeSettings",1f);
    }

    public GameObject settingsPanel;
    public void settingsShow(){

        Time.timeScale=1;
        //startPanel.SetActive(false);
        
        startPanel.GetComponent<Animator>().SetBool("open",false);
        startPanel.GetComponent<Animator>().SetBool("close",true);

        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().SetBool("close",false);
        settingsPanel.GetComponent<Animator>().SetBool("open",true);
        Invoke("closeMenu2",1f);
        
    }


    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);
        runPanel.SetActive(true);
    }

    public void backToMenu(){
        
        restartGame=false;
        Time.timeScale=1;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartFunc(){

        restartGame=true;
        Time.timeScale=1;
        Application.LoadLevel(Application.loadedLevel);


    }


    private void updateScore(int plus){

        score+=plus;
        foreach(Text txt in scoreTxt){
            txt.text="Score:"+score.ToString();
        }

        if(score>PlayerPrefs.GetInt("best")){
            PlayerPrefs.SetInt("best",score);
            foreach(Text txt in bestTxt){
                txt.text="Best: "+PlayerPrefs.GetInt("best").ToString();
            }
        }

    }

    void closeMenu(){
        runPanel.SetActive(true);
        startPanel.GetComponent<Animator>().SetBool("close",false);
        startPanel.SetActive(false);
    }

    void closeMenu2(){
        //runPanel.SetActive(true);
        startPanel.GetComponent<Animator>().SetBool("close",false);
        startPanel.SetActive(false);
    }

    void closeSettings(){
        settingsPanel.GetComponent<Animator>().SetBool("close",false);
        settingsPanel.SetActive(false);
    }

public static bool show=true;
private bool showedOnce=false;
    void closeRunPanel(){
        if(show && runPanel.activeSelf && !showedOnce) {
            if(!ads.showVideo()){
                admob.showIntersitionalAd();
            }
            
            showedOnce=true;
            show=!show;
            
        }

        else if(runPanel.activeSelf){
            show=!show;
        }
        runPanel.SetActive(false);
        diePanel.SetActive(true);
    }








    ///


    public void backFromShop(){
        shopPanel.SetActive(true);
        startPanel.SetActive(true);
        startPanel.GetComponent<SkinController>().changeSkin();
        startPanel.GetComponent<Animator>().SetBool("close",false);
        startPanel.GetComponent<Animator>().SetBool("open",true);

        shopPanel.GetComponent<Animator>().SetBool("open",false);
        shopPanel.GetComponent<Animator>().SetBool("close",true);
        
        Invoke("closeShop",1f);
    }

    public GameObject shopPanel;
    public void shopShow(){

        Time.timeScale=1;
        //startPanel.SetActive(false);
        
        startPanel.GetComponent<Animator>().SetBool("open",false);
        startPanel.GetComponent<Animator>().SetBool("close",true);

        shopPanel.SetActive(true);
        shopPanel.GetComponent<Animator>().SetBool("close",false);
        shopPanel.GetComponent<Animator>().SetBool("open",true);
        Invoke("closeMenu2",1f);
        
    }



    void closeShop(){
        shopPanel.GetComponent<Animator>().SetBool("close",false);
        shopPanel.SetActive(false);
    }


}

        // if(other.gameObject.tag=="ground" && !alive){
        //     bool playDie=true;
        //     if(childCubes.Count>0){
        //         foreach(var obj in childCubes){
        //             if(obj.GetComponent<NewCube>().grounded){
        //                 playDie=false;
        //             }
        //         }
        //         if(!playDie)return;
        //         foreach(var obj in childCubes)obj.transform.parent=null;
        //     }
        // }