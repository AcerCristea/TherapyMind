using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardController : MonoBehaviour
{
    public GameObject target;
    public GameObject parentPuzzle;

    private Collider theCollider;
    [SerializeField] private AngerRoomManager roomManager;
    [SerializeField] private float distance = 3f;
    private float maxDistance = 10.5f;
    private float minDistance = 1f;
    private bool snappy = false;

    void Start()
    {
        theCollider = GetComponent<Collider>();
        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<AngerRoomManager>();
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
        // move shard based on mouse scroll
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
    }
}