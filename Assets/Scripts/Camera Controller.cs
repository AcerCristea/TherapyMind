using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameraArray;
    public static int activeCamIndex = 0;
    private GameObject currentRoom;  // Current active room
    private AudioListener currentAudioListener;

    public GameObject northCam;
    public GameObject westCam;
    public GameObject southCam;
    public GameObject eastCam;

    public static CameraController instance;

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

        if (!RoomManager.instance.activePuzzle)
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

                RoomManager.instance.activeCamera = cameraArray[i].GetComponent<Camera>();
                RoomManager.instance.prevCamera = cameraArray[i].GetComponent<Camera>();

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

    public void SetCurrentRoom(GameObject room)
    {

        if (currentRoom != null)
        {
            currentRoom.SetActive(false); // Deactivate the current room
        }

        currentRoom = room;
        currentRoom.SetActive(true); // Activate the new room


        // Get all Camera components from the room
        Camera[] cameras = currentRoom.GetComponentsInChildren<Camera>(true);

        // Convert the Camera components to GameObjects and store them in the cameraArray
        cameraArray = new GameObject[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameraArray[i] = cameras[i].gameObject;
        }

        UpdateCamera(activeCamIndex);
    }

    // Activate the puzzle clicked
    public void SnapCamToPuzzle(Camera activeCam)
    {
        Ray ray = activeCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Puzzle")
            {
                // turns off all wall cams
                UpdateCamera(-1);
                RoomManager.instance.prevCamera = cameraArray[activeCamIndex].GetComponent<Camera>();
                RoomManager.instance.ActivatePuzzle(hit.collider.transform.parent.gameObject);
            }
        }
    }
}
