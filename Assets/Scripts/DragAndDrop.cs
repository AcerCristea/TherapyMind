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
        switch (CameraController.activeCamIndex)
        {
            case 0:
                MoveFromCam(northCam);
                break;
            case 1:
                MoveFromCam(westCam);
                break;
            case 2:
                MoveFromCam(eastCam);
                break;
            case 3:
                MoveFromCam(southCam);
                break;
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
