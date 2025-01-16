using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    void Start()
    {
        roomManager = FindFirstObjectByType<RoomManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (roomManager.activePuzzle == this.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }
        }
    }
}
