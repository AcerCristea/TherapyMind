using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objToSpin;
    public float rotateSpeed = 50f;
    public Vector3 right = new Vector3(0,0,1);
    public Vector3 left = new Vector3(0,0,-1);

    void Start()
    {
        
    }

    public void rotateRight(GameObject obj){
        obj.transform.Rotate(rotateSpeed * right * Time.deltaTime); 
    }

    public void rotateLeft(GameObject obj){
        obj.transform.Rotate(rotateSpeed * left * Time.deltaTime);
    }
}
