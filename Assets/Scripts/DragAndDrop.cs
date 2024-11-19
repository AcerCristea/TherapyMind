using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Camera northCam;
    public Camera westCam;
    public Camera eastCam;
    public Camera southCam;

    // [SerializeField] private int activeCam;

    private float distance = 3f;

    void OnMouseDrag()
    {
        // Get the active camera from the CameraController
        GameObject activeCamObj = null;

        // Check if the active camera index is valid
        if (CameraController.activeCamIndex >= 0 && CameraController.activeCamIndex < CameraController.instance.cameraArray.Length)
        {
            activeCamObj = CameraController.instance.cameraArray[CameraController.activeCamIndex];
        }

        if (activeCamObj != null)
        {
            Camera activeCam = activeCamObj.GetComponent<Camera>();
            if (activeCam != null)
            {
                MoveFromCam(activeCam);
            }
        }
    }
    // TODO:
    // bound the box to the room
    // 
    private void MoveFromCam(Camera activeCam)
    {
        distance += Input.mouseScrollDelta.y;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = activeCam.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
}
