using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class AngerCameraController : MonoBehaviour
{
    public GameObject[] cameraArray;
    public static int activeCamIndex = 0;
    private GameObject currentRoom;  // Current active room
    private AudioListener currentAudioListener;

    public GameObject northCam;
    public GameObject westCam;
    public GameObject southCam;
    public GameObject eastCam;

    public static AngerCameraController instance;

    // For the Drag and Drop
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assuming the first room is set up as active in the scene
        cameraArray = new GameObject[] { northCam, westCam, southCam, eastCam };
        UpdateCamera(activeCamIndex);


    }

    void Update()
    {

        if (!AngerRoomManager.instance.activePuzzle)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LeftButton();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                RightButton();
            }

            if (Input.GetMouseButtonDown(0))
            {
                SnapCamToPuzzle(cameraArray[activeCamIndex].GetComponent<Camera>());
            }
        }
    }

    // UpdateCamera keeps only one camera on
    public void UpdateCamera(int index)
    {
        for (int i = 0; i < cameraArray.Length; i++)
        {
            cameraArray[i].SetActive(i == index);

            if (i == index)
            {

                AngerRoomManager.instance.activeCamera = cameraArray[i].GetComponent<Camera>();
                AngerRoomManager.instance.prevCamera = cameraArray[i].GetComponent<Camera>();

                // Ensure only one AudioListener is active
                AudioListener listener = cameraArray[i].GetComponent<AudioListener>();
                if (listener != null)
                {
                    if (currentAudioListener != null)
                    {
                        currentAudioListener.enabled = false;
                    }
                    listener.enabled = true;
                    currentAudioListener = listener;
                }

                activeCamIndex = i;
            }
        }
    }

    // Activate the camera to the left
    public void LeftButton()
    {
        activeCamIndex--;
        if (activeCamIndex < 0)
        {
            activeCamIndex = cameraArray.Length - 1;
        }
        UpdateCamera(activeCamIndex);
    }

    // Activate the camera to the right
    public void RightButton()
    {
        activeCamIndex++;
        if (activeCamIndex >= cameraArray.Length)
        {
            activeCamIndex = 0;
        }
        UpdateCamera(activeCamIndex);
    }

    public void SetCurrentRoom(GameObject room, string targetCameraName)
    {
        Debug.Log("SetCurrentRoom called with room: " + room.name + " and targetCameraName: " + targetCameraName);

        if (currentRoom != null)
        {
            currentRoom.SetActive(false); // Deactivate the current room
            Debug.Log("Deactivating current room: " + currentRoom.name);
        }

        currentRoom = room;
        currentRoom.SetActive(true); // Activate the new room
        Debug.Log("Activating new room: " + room.name);

        // Get all Camera components from the room
        Camera[] cameras = currentRoom.GetComponentsInChildren<Camera>(true);

        // Convert the Camera components to GameObjects and store them in the cameraArray
        cameraArray = new GameObject[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameraArray[i] = cameras[i].gameObject;
            Debug.Log("Camera added to array: " + cameraArray[i].name);
        }

        // Sort the camera array based on camera names or any desired property
        cameraArray = SortCameras(cameraArray);

        // Ensure activeCamIndex is within bounds
        if (activeCamIndex >= cameraArray.Length)
        {
            activeCamIndex = 0; // Reset to first camera if out of bounds
            Debug.Log("activeCamIndex was out of bounds, resetting to 0.");
        }

        // Debugging: Log the names of all cameras in the array after sorting
        Debug.Log("Camera array contains the following cameras (sorted):");
        foreach (var camera in cameraArray)
        {
            Debug.Log(" - " + camera.name);
        }

        // Find the target camera by name
        GameObject targetCamera = null;
        foreach (GameObject camera in cameraArray)
        {
            if (camera.name == targetCameraName)
            {
                targetCamera = camera;
                break;
            }
        }

        if (targetCamera != null)
        {
            Debug.Log("Found target camera: " + targetCamera.name);
            int targetIndex = System.Array.IndexOf(cameraArray, targetCamera);
            UpdateCamera(targetIndex);
        }
        else
        {
            Debug.LogWarning("Target camera not found! Using activeCamIndex: " + activeCamIndex);
            UpdateCamera(activeCamIndex); // If not found, default to activeCamIndex
        }
    }

    // Helper method to sort cameras based on names
    private GameObject[] SortCameras(GameObject[] cameras)
    {
        // Sort cameras based on their name (assuming they follow a naming convention like "North", "South", etc.)
        System.Array.Sort(cameras, (a, b) => a.name.CompareTo(b.name));

        // Log sorted array for debugging
        Debug.Log("Cameras sorted by name:");
        foreach (var camera in cameras)
        {
            Debug.Log(" - " + camera.name);
        }

        return cameras;
    }


    // Activate the puzzle clicked
    public void SnapCamToPuzzle(Camera activeCam)
    {
        Ray ray = activeCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Ray hit: " + hit.collider.gameObject.name);  // Debug statement

            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Puzzle") || hitObject.transform.root.CompareTag("Puzzle"))
            {
                //Debug.Log("Puzzle clicked: " + hit.collider.gameObject.name);  // Debug statement

                // turns off all wall cams
                UpdateCamera(-1);
                GameObject puzzle = hitObject.transform.root.gameObject;  // Access the root (parent) sink object

                AngerRoomManager.instance.prevCamera = cameraArray[activeCamIndex].GetComponent<Camera>();
                AngerRoomManager.instance.ActivatePuzzle(puzzle);
            }
        }
    }
}
