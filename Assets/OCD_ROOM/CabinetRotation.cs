using System;
<<<<<<< Updated upstream
=======
// using UnityEditor.EditorTools;
>>>>>>> Stashed changes
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
            initialRotation = hingePoint.localRotation.eulerAngles.y;
            currentRotation = initialRotation; // Sync currentRotation with the hinge's initial rotation
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

    private float currentRotation = 0f; // Track the rotation angle manually

<<<<<<< Updated upstream
    void HandleRotation()
    {
        // Get input from arrow keys
        float rotationInput = 0f;
=======
void HandleRotation()
{
    // Get input from arrow keys
    float rotationInput = 0f;

    if (Input.GetKey(KeyCode.D))
    {
        rotationInput = -1f; // Rotate clockwise
    }
    else if (Input.GetKey(KeyCode.A))
    {
        rotationInput = 1f; // Rotate counterclockwise
    }
>>>>>>> Stashed changes

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = -1f; // Rotate clockwise
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = 1f; // Rotate counterclockwise
        }

        // Calculate the new rotation
        if (rotationInput != 0f)
        {
            currentRotation += rotationInput * rotationSpeed * Time.deltaTime;

            // Clamp the rotation between 0 and 90 degrees
            currentRotation = Mathf.Clamp(currentRotation, 0f, 90f);

            // Apply the rotation to the hinge point
            if (hingePoint != null)
            {
                hingePoint.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
            }

            // Check for task completion
            CheckTaskCompletion(currentRotation);
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
