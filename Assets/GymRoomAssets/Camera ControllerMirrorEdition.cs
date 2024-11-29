using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControllerMirrorEdition : MonoBehaviour
{
    public static GameObject northCam;
    public static GameObject westCam;
    public static GameObject eastCam;
    public static GameObject southCam;

    public static int activeCamIndex = 0;

    public GameObject[] cameraArray = { northCam, westCam, southCam, eastCam };

    void Start()
    {
        updateCamera(activeCamIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftButton();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rightButton();
        }
    }
    // UpdateCamera keeps only one camera on
    private void updateCamera(int index)
    {
        for (int i = 0; i < cameraArray.Length; i++)
        {
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
    public void leftButton()
    {
        activeCamIndex--;
        if (activeCamIndex < 0)
        {
            activeCamIndex = cameraArray.Length - 1;
        }
        updateCamera(activeCamIndex);
    }
    // Activate the camera to the right
    public void rightButton()
    {
        activeCamIndex++;
        if (activeCamIndex > cameraArray.Length - 1)
        {
            activeCamIndex = 0;
        }
        updateCamera(activeCamIndex);

    }
    public void SnapCamToPuzzle()
    {
        /*
        When the player clicks on anything within a puzzle, they're transported to
        the camera closest to that puzzle

        handle player clicking on puzzle area/objects
            raytrace?
        make call to RoomManager.instance.MoveToPuzzle
        */
    }
}
