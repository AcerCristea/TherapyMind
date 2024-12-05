using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardController : MonoBehaviour
{
    public GameObject target;
    public GameObject parentPuzzle;


    [Header("spacer")]

    public GameObject backDrop;
    public GameObject itsTheCamera;

    private Collider theCollider;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private float distance = 3f;

    private float maxDistance = 3f;
    private float minDistance = 1f;
    private bool snappy = false;


    private Vector3 intialPos;


    void Start()
    {
        theCollider = GetComponent<Collider>();
        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<RoomManager>();
        if (checkSnap())
        {
            SnapToTarget();
            MegaShardController.correctCounter++;
        }
        maxDistance = Vector3.Distance(itsTheCamera.transform.position, backDrop.transform.position);


        initialPos = transform.position;

    }

    void Update()
    {
        if (roomManager.activePuzzle == parentPuzzle)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }
        }
    }

    void OnMouseDrag()
    {
        //need to make it so puzzle doesn't work when not on the right cam;
        if (roomManager.activePuzzle == parentPuzzle)
        {
            MoveFromCam(roomManager.activeCamera);
        }

    }

    void OnMouseUp()
    {
        if (checkSnap())
        {
            SnapToTarget();
            MegaShardController.correctCounter++;
        }
        else
        {
            returnBack();
        }
    }
    // TODO:
    // bound the box to the room
    // 
    private void MoveFromCam(Camera activeCam)
    {
        distanceKeeper();
        // move shard based on mouse input
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = activeCam.ScreenToWorldPoint(mousePosition);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = objPosition;

    }

    private void SnapToTarget()
    {
        transform.position = target.transform.position;
        theCollider.enabled = false;
        Debug.Log("Just snapped to mirror");
    }

    private bool checkSnap()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5)
        {
            snappy = true;
        }
        return snappy;
    }

    private void distanceKeeper()
    {
        if (distance <= maxDistance && distance >= minDistance)
        {
            distance += Input.mouseScrollDelta.y;
        }
        // keep shards within room
        if (distance > maxDistance)
        {
            distance = maxDistance;
        }
        if (distance < minDistance)
        {
            distance = minDistance;
        }
    }

    private void returnBack()
    {
        transform.position = initialPos;
    }
}
