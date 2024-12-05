using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveManager : MonoBehaviour
{
    public GameObject leftMarker;
    public GameObject middleMarker;
    public GameObject rightMarker;

    public GameObject leftGreen;
    public GameObject middleGreen;
    public GameObject rightGreen;

    private float leftDistance;
    private float middleDistance;
    private float rightDistance;

    private bool leftCheck = false;
    private bool middleCheck = false;
    private bool rightCheck = false;

    public GameObject theDoor;
    public GameObject valvePuzzle;

    public static ValveManager instance;
    [SerializeField] private RoomManager roomManager;

    void Awake()
    {
        instance = this;
        roomManager = FindFirstObjectByType<RoomManager>();
    }

    void Update()
    {
        if (roomManager.activePuzzle == valvePuzzle)
        {
            // Win condition
            if (checkEachWheel())
            {
                theDoor.SetActive(false);
                Debug.Log("VALVES DONE, checked in SuanaRoomDistance");
            }
            // ESC to exit puzzle
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }
        }
    }

    public bool checkEachWheel()
    {
        leftDistance = Vector3.Distance(leftMarker.transform.position, leftGreen.transform.position);
        if (leftDistance < 0.38 && leftDistance > 0.365)
        {
            leftCheck = true;
        }

        middleDistance = Vector3.Distance(middleMarker.transform.position, middleGreen.transform.position);
        if (middleDistance < 0.34 && middleDistance > 0.33)
        {
            middleCheck = true;
        }

        rightDistance = Vector3.Distance(rightMarker.transform.position, rightGreen.transform.position);
        if (rightDistance < 0.293 && rightDistance > 0.289)
        {
            rightCheck = true;
        }

        if (leftCheck && middleCheck && rightCheck)
        {
            return true;
        }
        return false;
    }




}