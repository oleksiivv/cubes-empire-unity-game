using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCube : MonoBehaviour
{
    private GameObject player;
    public GameObject Player{set{
        player=value;
    }}

    private Rigidbody rigidbody;

    public float speed{get;set;}

    private bool runCube=true;

    private PlayerController playerController;

    public GameObject alive;
    private AlivePlane box;

    public bool thisEntrance;

    void Start()
    {
        box=alive.GetComponent<AlivePlane>();
        thisEntrance=false;
        playerController=player.GetComponent<PlayerController>();

        Invoke(nameof(clean),10f);

        runCube=true;
        rigidbody=GetComponent<Rigidbody>();
        var relativePos = player.transform.position - transform. position;
        var rotation = Quaternion. LookRotation(relativePos). eulerAngles;
        transform. rotation = Quaternion. Euler(0,rotation.y,0);
        
    }
    void Update()
    {
     
        if((box.isEmpty() || thisEntrance) && transform.parent!=player.transform && playerController.alive && runCube && !playerController.GetComponent<PlayerController>().cantDie){
            
            transform.Translate(Vector3.forward*speed*Time.timeScale);
        }
    }

    
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="newcube" && transform.parent!=player){
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="newcube" || other.gameObject.tag=="Player"){
            runCube=false;
            //Debug.Log("Box fucking exit");
            //box.Exit();
            //thisEntrance=false;
        }

    
    }


    void clean(){
        if(transform.parent!=player.transform && playerController.alive){
            box.Exit();
            Destroy(gameObject);
        }
        else{
            Invoke(nameof(clean),10f);         
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            runCube=false;
            box.Exit();
            Debug.Log("Box fucking exit");
            thisEntrance=false;
        }

        if(other.tag=="AlivePlane"){
            if(box.isEmpty() && runCube && !thisEntrance && transform.parent!=player.gameObject){
                box.Entrance();
                thisEntrance=true;
            }
        }
    }


    void OnTriggerExit(Collider other){
     
        if(other.tag=="AlivePlane"){
            box.Exit();
            Debug.Log("Box fucking exit");
            thisEntrance=false;
        }
    }


    void OnCollisionStay(Collision other){
        if(other.gameObject.tag=="newcube" || other.gameObject.tag=="Player"){
            runCube=false;
            box.Exit();
            Debug.Log("Box fucking exit");
            thisEntrance=false;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="newcube" || other.gameObject.tag=="Player"){
            runCube=true;
        }

    }
}












   

        /*transform.position=Vector3.MoveTowards(transform.position,
                            new Vector3(player.transform.position.x,
                                        transform.position.y,
                                        player.transform.position.z),speed);*/

        //rigidbody.AddForce(Vector3.forward*50);

        //var relativePos = player.transform.position - transform. position;
            //var rotation = Quaternion. LookRotation(relativePos). eulerAngles;
            //transform. rotation = Quaternion. Euler(0,rotation.y,0);
            //transform.LookAt(player.transform);

            // transform.eulerAngles=new Vector3(transform.eulerAngles.x,-player.transform.eulerAngles.y,transform.eulerAngles.z);
            // transform.position=Vector3.MoveTowards(transform.position,
            //                 new Vector3(player.transform.position.x,
            //                             transform.position.y,
            //                             player.transform.position.z),speed);
