using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public Camera activeCamera;
    public Camera prevCamera;

    public GameObject activePuzzle;
    public List<GameObject> puzzleList;

    void Awake()
    {
        instance = this;
    }
    // place the camera in front of the puzzle clicked
    public void ActivatePuzzle(GameObject puzzle)
    {
        // search puzzle for the attached camera then switch to it
        foreach (Transform child in puzzle.transform)
        {
            if (child.GetComponent<Camera>() != null)
            {
                child.gameObject.SetActive(true);
                activeCamera = child.GetComponent<Camera>();
            }
        }
        // set activePuzzle to puzzle
        activePuzzle = puzzle;
    }
    // place the camera back on the wall
    public void ReturnToWall()
    {
        activeCamera.gameObject.SetActive(false);
        prevCamera.gameObject.SetActive(true);
        activeCamera = prevCamera;
        prevCamera = null;
        activePuzzle = null;
    }
}
