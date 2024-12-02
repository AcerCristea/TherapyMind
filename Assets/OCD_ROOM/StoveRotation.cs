using System;
using UnityEngine;

public class StoveRotation : MonoBehaviour
{
    public GameObject burner;  // Reference to the child burner object
    public GameObject flameEffect;  // Reference to the flame effect object (e.g., fire particle system)

    public float rotationSpeed = 50f;

    // Target angle for task completion
    public float targetRotation = -90f;

    public bool isTaskComplete = false;  // Track if the burner is turned off (task complete)

    [SerializeField] private RoomManager roomManager;  // Reference to the RoomManager
    [SerializeField] private GameManager gameManager;  // Reference to the GameManager

    private float initialRotation;  // Stores the starting rotation

    public AudioSource stoveAudio;


    void Start()
    {
        // Find the RoomManager and GameManager in the scene
        roomManager = FindFirstObjectByType<RoomManager>();
        gameManager = FindFirstObjectByType<GameManager>();

        // Record the initial Y-axis rotation of the burner
        if (burner != null)
        {
            initialRotation = burner.transform.rotation.eulerAngles.y;
        }
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
        float rotationY = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationY = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationY = -1f;
        }

        // Rotate the burner
        if (burner != null && rotationY != 0f)
        {
            float newRotationY = burner.transform.rotation.eulerAngles.y + (rotationY * rotationSpeed * Time.deltaTime);

            // Apply the rotation
            burner.transform.rotation = Quaternion.Euler(
                burner.transform.rotation.eulerAngles.x,
                newRotationY,
                burner.transform.rotation.eulerAngles.z
            );

            // Check for task completion
            CheckTaskCompletion(newRotationY);
        }
    }

    void CheckTaskCompletion(float currentRotation)
    {
        // Normalize rotation relative to initial rotation
        float normalizedRotation = Mathf.Repeat(currentRotation - initialRotation, 360f);

        // Convert targetRotation to Unity's angle range [0, 360]
        float adjustedTargetRotation = Mathf.Repeat(targetRotation, 360f);

        if (!isTaskComplete && Mathf.Abs(normalizedRotation - adjustedTargetRotation) < 5f) // Allow small tolerance
        {
            // Turn off the flame effect and mark the task as complete
            if (flameEffect != null)
            {
                flameEffect.SetActive(false);
            }

            isTaskComplete = true;
            gameManager.MarkTaskAsComplete("Burner");

            if (stoveAudio.isPlaying)
            {
                stoveAudio.Stop();
            }

            Debug.Log("Burner turned off and task marked as complete!");
        }
        else if (isTaskComplete && Mathf.Abs(normalizedRotation - adjustedTargetRotation) >= 5f)
        {
            // Turn on the flame effect and mark the task as incomplete
            if (flameEffect != null)
            {
                flameEffect.SetActive(true);
            }

            isTaskComplete = false;
            gameManager.MarkTaskAsIncomplete("Burner");

            if (!stoveAudio.isPlaying)
            {
                stoveAudio.Play();
            }

            Debug.Log("Burner moved away from off position. Task marked as incomplete.");
        }
    }
}
