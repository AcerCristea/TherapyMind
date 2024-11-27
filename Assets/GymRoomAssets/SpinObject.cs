using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    
    public float Speed = 10f;
    private bool isRotating = false;
    private float startMousePositionX;
    private float startMousePositionY;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            isRotating = true;
            startMousePositionX = Input.mousePosition.x;
            startMousePositionY = Input.mousePosition.y;
        }
        else if(Input.GetMouseButtonUp(0)){
            isRotating = false;
        }

        if(isRotating){
            float currentMousePositionX = Input.mousePosition.x;
            float currentMousePositionY = Input.mousePosition.y;
            float mouseMovement = (currentMousePositionX - startMousePositionX) - (currentMousePositionY - startMousePositionY);

            transform.Rotate(Vector3.forward, mouseMovement * Speed * Time.deltaTime);
            startMousePositionX = currentMousePositionX;
            startMousePositionY = currentMousePositionY;
        }
    }
}
