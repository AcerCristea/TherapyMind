using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SinkRotation : MonoBehaviour
{
    // Speed of rotation

    public GameObject faucet;  // Reference to the child object

    public GameObject water;
    public GameObject waterSink;

    public float rotationSpeed = 50f;

    // Rotation limits
    public float minRotation = 0f;
    public float maxRotation = 30.7f;

    public bool isTaskComplete = false; // Track if the task is completed

    [SerializeField] private RoomManager roomManager; // Reference to the RoomManager
    [SerializeField] private GameManager gameManager; // Reference to the GameManager

    public AudioSource runningWaterAudio;
    public AudioSource sinkOffAudio;

    void Start()
    {
        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<RoomManager>();
        gameManager = FindFirstObjectByType<GameManager>();

    }

    void Update()
    {
        if (roomManager.activePuzzle == this.gameObject)
        {
            HandleRotation();

            // Exit puzzle and return to the main camera
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }
        }
    }

    void HandleRotation()
    {
        // Get input from arrow keys
        float rotationX = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            rotationX = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotationX = -1f;
        }

        // Rotate the object
        if (faucet != null && rotationX != 0f)
        {
            float newRotationX = faucet.transform.rotation.eulerAngles.x + (rotationX * rotationSpeed * Time.deltaTime);

            // Clamp the rotation to the allowed range
            newRotationX = Mathf.Clamp(newRotationX, minRotation, maxRotation);

            // Apply the clamped rotation
            faucet.transform.rotation = Quaternion.Euler(newRotationX, faucet.transform.rotation.eulerAngles.y, faucet.transform.rotation.eulerAngles.z);

            // Handle task completion
            CheckTaskCompletion(newRotationX);
        }
    }

    void CheckTaskCompletion(float currentRotation)
    {
        if (!isTaskComplete && Mathf.Approximately(currentRotation, minRotation))
        {
            water.SetActive(false);
            waterSink.SetActive(false);
            isTaskComplete = true;
            gameManager.MarkTaskAsComplete("Faucet");

            if (runningWaterAudio.isPlaying)
            {
                runningWaterAudio.Stop();
            }
            sinkOffAudio.Play();

            Debug.Log("Faucet turned off and task marked as complete!");
        }
        else if (isTaskComplete && !Mathf.Approximately(currentRotation, minRotation))
        {
            water.SetActive(true);
            waterSink.SetActive(true);
            isTaskComplete = false;
            gameManager.MarkTaskAsIncomplete("Faucet");
            if (!runningWaterAudio.isPlaying)
            {
                runningWaterAudio.Play();
            }

            Debug.Log("Faucet moved away from off position. Task marked as incomplete.");
        }
    }

}
