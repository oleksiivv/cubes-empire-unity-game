using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlivePlane : MonoBehaviour
{
    // Start is called before the first frame update
    public bool cubesInside;

    void Start(){
        cubesInside=false;
    }

    public void Entrance(){
        cubesInside=true;
        
    }

    public bool Exit(){

        this.cubesInside=false;
        return cubesInside;

    }

    public bool isEmpty(){
        return !cubesInside;
    }

}
