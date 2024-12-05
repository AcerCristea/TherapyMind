using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject targetRoom;  // Prefab for the next room
    public string targetCameraName;    // The name of the target camera in the next room
    public Transform hingePoint; // Hinge point for the door
    public float openAngle = 20f; // Angle the door will rotate to
    public float animationDuration = 0.5f; // Time it takes to animate the door opening
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Coroutine currentAnimation;
    public AudioSource burner;
    public AudioSource tap;



    private void Start()
    {
        initialRotation = hingePoint.rotation; // Store the initial rotation
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0)); // Calculate the target rotation

    }

    private void OnMouseEnter()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateDoor(initialRotation, targetRotation, animationDuration));
    }

    private void OnMouseExit()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateDoor(hingePoint.rotation, initialRotation, animationDuration));
    }

    private IEnumerator AnimateDoor(Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            hingePoint.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        hingePoint.rotation = endRotation; // Ensure the exact end rotation is set
    }
    private void OnMouseDown()
    {
        Debug.Log("Door clicked");

        // Use FindAnyObjectByType to get the CameraController
        CameraController cameraController = FindAnyObjectByType<CameraController>();
        GameManager gameManager = Object.FindAnyObjectByType<GameManager>();

        if (cameraController != null)
        {
            Debug.Log("CameraController found, transitioning room");

            gameManager.StopTimer();

            Debug.Log("Passing targetCameraName to CameraController: " + targetCameraName);


            // Set the current room to the new room
            cameraController.SetCurrentRoom(targetRoom, targetCameraName);

            Debug.Log("TargetRoomName: " + targetRoom.name);

            if (targetRoom.name == "OcdRoom")
            {
                gameManager.ResetAndStartTimer();

                Debug.Log("RESULTSSS: " + gameManager.isBurnerTaskComplete);
                Debug.Log("RESULTSSS: " + gameManager.isFaucetTaskComplete);
                if (!gameManager.isBurnerTaskComplete)
                {
                    burner.Play();
                }

                if (!gameManager.isFaucetTaskComplete)
                {
                    tap.Play();
                }

            }
            if (targetRoom.name != "OcdRoom")
            {
                burner.Stop();
                tap.Stop();
            }

        }
        else
        {
            Debug.LogError("CameraController not found!");
        }
    }
}
