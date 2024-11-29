using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropMirrorEdition : MonoBehaviour
{
    public Camera puzzleCam;

    public GameObject target;

    private Collider theCollider;
    // [SerializeField] private int activeCam;
    [SerializeField] private float distance = 3f;
    private float maxDistance = 10.5f;
    private float minDistance = 1f;
    private bool snappy = false;

    void Start()
    {
        theCollider = GetComponent<Collider>();
    }

    void OnMouseDrag()
    {
        //need to make it so puzzle doesn't work when not on the right cam;
        MoveFromCam(puzzleCam);
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5)
        {
            snappy = true;
        }
        if (snappy)
        {
            SnapToTarget();
        }
    }
    // TODO:
    // bound the box to the room
    // 
    private void MoveFromCam(Camera activeCam)
    {
        if (distance <= maxDistance && distance >= minDistance)
        {
            distance += Input.mouseScrollDelta.y;
        }
        if (distance > maxDistance)
        {
            distance = maxDistance;
        }
        if (distance < minDistance)
        {
            distance = minDistance;
        }
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = activeCam.ScreenToWorldPoint(mousePosition);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = objPosition;

    }

    private void SnapToTarget()
    {
        transform.position = target.transform.position;
        theCollider.enabled = false;
    }
}
