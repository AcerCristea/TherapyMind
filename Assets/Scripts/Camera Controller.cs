using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static GameObject northCam;
    public static GameObject westCam;
    public static GameObject eastCam;
    public static GameObject southCam;

    public static int activeCamIndex = 0;

    public GameObject[] cameraArray = {northCam, westCam, southCam, eastCam};

    void Start()
    {
        UpdateCamera(activeCamIndex);
    }

    void Update()
    {
        // traverse wall cameras on A/D
        if (Input.GetKeyDown(KeyCode.A))
        {
            SnapCamLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SnapCamRight();
        }
        // move to puzzle on LMB click
        if (Input.GetMouseButtonDown(0))
        {
            SnapCamToPuzzle(cameraArray[activeCamIndex].GetComponent<Camera>());
        }
    }
    // UpdateCamera keeps only one camera on
    private void UpdateCamera(int index){
        for(int i = 0; i < cameraArray.Length; i++){
            // only use the active camera 
            if (i == index)
            {
                cameraArray[i].SetActive(true);
                activeCamIndex = i;
                // CameraManager.instance.activeCamera = cameraArray[i].GetComponent<Camera>();
            } 
            else
            {
                // turn off every other camera in scene
                if (cameraArray[i] != null)
                {
                    cameraArray[i].SetActive(false);
                }
            }
        }
    }
    // Activate the camera to the left
    public void SnapCamLeft(){
        activeCamIndex--;
        if (activeCamIndex < 0)
        {
            activeCamIndex = cameraArray.Length - 1;
        }
        UpdateCamera(activeCamIndex);
    }
    // Activate the camera to the right
    public void SnapCamRight(){
        activeCamIndex++;
        if (activeCamIndex > cameraArray.Length - 1)
        {
            activeCamIndex = 0;
        }
        UpdateCamera(activeCamIndex);

    }
    // Activate the camera in front of the puzzle clicked
    /*
    When the player clicks on anything within a puzzle, they're transported to
    the camera closest to that puzzle
    
    handle player clicking on puzzle area/objects
        raytrace?
    make call to RoomManager.instance.MoveToPuzzle
    */
    public void SnapCamToPuzzle(Camera activeCam)
    {
        Ray ray = activeCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Puzzle")
            {
                Debug.Log("puzzle hit");
            }
        }
    }
}
