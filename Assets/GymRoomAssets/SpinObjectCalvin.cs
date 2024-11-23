using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectCalvin : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCam;

    private float distance;
    void Start()
    {
        distance = Vector3.Distance(mainCam.transform.position, transform.position);
    }

    // Update is called once per frame
   void OnMouseDrag(){
    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
    Vector3 objPosition = mainCam.ScreenToWorldPoint(mousePosition);
    transform.position = objPosition;
   }
}
