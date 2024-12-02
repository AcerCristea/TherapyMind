using System;
using UnityEditor.EditorTools;
using UnityEngine;

public class CabinetRotation : MonoBehaviour
{
    public Transform hingePoint; // Hinge point for the door
    public float rotationSpeed = 50f; // Speed of rotation (degrees per second)

    // Target angle for task completion
    public float targetRotation = 0f;

    public GameObject cabinet;

    private bool isTaskComplete = false; // Track if the door is rotated to the correct position
    private float initialRotation; // Store the starting rotation

    [SerializeField] private GameManager gameManager; // Reference to the GameManager
    [SerializeField] private RoomManager roomManager; // Reference to the GameManager

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();
        roomManager = FindFirstObjectByType<RoomManager>();

        // Record the initial Y-axis rotation of the hinge point
        if (hingePoint != null)
        {
            initialRotation = hingePoint.rotation.eulerAngles.y;
        }
        else
        {
            Debug.LogError("Hinge Point is not assigned!");
        }
    }

    void Update()
    {
        if (roomManager.activePuzzle == cabinet)
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
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = -1f; // Rotate clockwise
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = 1f; // Rotate counterclockwise
        }

        // Rotate the door
        if (hingePoint != null && rotationInput != 0f)
        {
            float newRotationY = hingePoint.rotation.eulerAngles.y + (rotationInput * rotationSpeed * Time.deltaTime);

            newRotationY = Mathf.Clamp(newRotationY, 0f, 90f);


            // Apply the rotation
            hingePoint.rotation = Quaternion.Euler(
                hingePoint.rotation.eulerAngles.x,
                newRotationY,
                hingePoint.rotation.eulerAngles.z
            );

            // Check for task completion
            CheckTaskCompletion(newRotationY);
        }
    }

    void CheckTaskCompletion(float currentRotation)
    {


        if (!isTaskComplete && Mathf.Abs(currentRotation) < 3f) // Allow small tolerance
        {
            // Mark the task as complete
            isTaskComplete = true;
            gameManager.MarkTaskAsComplete("Cabinet");
            Debug.Log("Cabinet task marked as complete!");
        }
        else if (isTaskComplete && Mathf.Abs(currentRotation) >= 3f)
        {
            // Mark the task as incomplete
            isTaskComplete = false;
            gameManager.MarkTaskAsIncomplete("Cabinet");
            Debug.Log("Cabinet moved away from target position. Task marked as incomplete.");
        }
    }
}
