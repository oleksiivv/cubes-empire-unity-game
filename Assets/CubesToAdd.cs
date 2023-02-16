using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesToAdd : MonoBehaviour
{
    public GameObject[] cubes;
    public Vector2 horizontalX;
    public Vector2 verticalY;

    public bool play=true;
    public int interval=3;

    public float posDy=0;

    public Vector2 mapSizeY,mapSizeX;

    public GameObject player;
    public GameObject cube;


    public void startGame(){
        StartCoroutine(cubesInst());
    }


    IEnumerator cubesInst(){
        while(player.GetComponent<PlayerController>().alive){
            if(play && Time.timeScale!=0){
                int axis=Random.Range(0,4);
                GameObject newCube;

                List<int> var=new List<int>();
                var.Add(0);
                var.Add(Random.Range(0,cubes.Length));

                if(axis==0){
                    int id=var[Random.Range(0,2)];
                    newCube=Instantiate(cubes[id], new Vector3(horizontalX.x,
                                                        cubes[Random.Range(0,cubes.Length)].transform.position.y+posDy,
                                                        Random.Range(mapSizeY.x,mapSizeY.y)),cubes[id].transform.rotation) as GameObject;
                }
                else if(axis==1){

                    int id=var[Random.Range(0,2)];
                    newCube=Instantiate(cubes[id], new Vector3(horizontalX.y,
                                                        cubes[Random.Range(0,cubes.Length)].transform.position.y+posDy,
                                                        Random.Range(mapSizeY.x,mapSizeY.y)),cubes[id].transform.rotation) as GameObject;
                }
                else if(axis==2){
                    int id=var[Random.Range(0,2)];
                    newCube=Instantiate(cubes[id], new Vector3(Random.Range(mapSizeX.x,mapSizeX.y),
                                                        cubes[Random.Range(0,cubes.Length)].transform.position.y+posDy,
                                                        verticalY.x),cubes[id].transform.rotation) as GameObject;
                }
                else{
                    int id=var[Random.Range(0,2)];
                    newCube=Instantiate(cubes[id], new Vector3(Random.Range(mapSizeX.x,mapSizeX.y),
                                                        cubes[Random.Range(0,cubes.Length)].transform.position.y+posDy,
                                                        verticalY.y),cubes[id].transform.rotation) as GameObject;
                }
                

                newCube.GetComponent<NewCube>().Player=player;
                newCube.GetComponent<NewCube>().alive=cube;
                //newCube.GetComponent<NewCube>().speed=0.02f;
                if(newCube.gameObject.name.Contains("fast")){
                    newCube.GetComponent<NewCube>().speed=0.03f;
                }

                else if(newCube.gameObject.name.Contains("slow")){
                    newCube.GetComponent<NewCube>().speed=0.017f;
                }
                else{
                    newCube.GetComponent<NewCube>().speed=0.02f;
                }



            }
            yield return new WaitForSeconds(interval);
            
        }
    }


}
