using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Camera northCam;
    public Camera westCam;
    public Camera eastCam;
    public Camera southCam;

    [SerializeField] private int activeCam;

    private float distance = 3f;

    void OnMouseDrag()
    {
        if (CameraController.whichCam == 0)
        {
            northCamMovement();
        }
        else if (CameraController.whichCam == 1)
        {
            westCamMovement();
        }
        else if (CameraController.whichCam == 2)
        {
            eastCamMovement();
        }
        else if (CameraController.whichCam == 3)
        {
            southCamMovement();
        }
    }

    private void northCamMovement()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = northCam.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }

    private void westCamMovement()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = westCam.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }

    private void eastCamMovement()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = eastCam.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }

    private void southCamMovement()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = southCam.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }




}
