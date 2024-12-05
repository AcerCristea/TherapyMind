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
    [SerializeField] private GameManager gameManager;

    [SerializeField] ParticleSystem particleEffect1; // Drag your particle effect prefab here
    [SerializeField] ParticleSystem particleEffect2; // Drag your particle effect prefab here



    void Awake()
    {
        instance = this;
        roomManager = FindFirstObjectByType<RoomManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {

        if (roomManager.activePuzzle == valvePuzzle)
        {
            // Win condition
            if (checkEachWheel() && !GameManager.instance.valvePuzzleComplete)
            {
                GameManager.instance.DecreaseInsanity(20f); // Adjust the value as needed
                GameManager.instance.valvePuzzleComplete = true; // Adjust the value as needed

                theDoor.SetActive(false);
                Debug.Log("VALVES DONE, checked in SuanaRoomDistance");
                
                particleEffect1.Play(); // Drag your particle effect prefab here
                particleEffect2.Play(); // Drag your particle effect prefab here

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
        //Debug.Log("Left Distance: " + leftDistance);
        if (leftDistance < 0.8 && leftDistance > 0.73)
        {
            Debug.Log("Left Done:");
            leftCheck = true;
        }

        middleDistance = Vector3.Distance(middleMarker.transform.position, middleGreen.transform.position);
        //Debug.Log("middle Distance: " + middleDistance);
        if (middleDistance < 0.8)
        {
            middleCheck = true;
        }

        rightDistance = Vector3.Distance(rightMarker.transform.position, rightGreen.transform.position);
        Debug.Log("right Diatncae: " + rightDistance);
        if (rightDistance < 0.78)
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