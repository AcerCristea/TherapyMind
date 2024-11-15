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

    public void MoveToPuzzle(GameObject puzzle)
    {
        // search puzzle for the attached camera
        // disable stuff from other puzzles
        // enable stuff within puzzle
        // set prevCamera to the activeCamera
        // swtich activeCamera to the cam in front of puzzle
        // set activePuzzle to puzzle
        return;
    }
    public void ReturnToWall()
    {
        activeCamera = prevCamera;
        prevCamera = null;
    }
}
