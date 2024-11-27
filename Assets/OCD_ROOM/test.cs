using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager; // Reference to the RoomManager


    void Start()
    {
        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<RoomManager>();


    }

    void Update()
    {
        if (roomManager.activePuzzle == this.gameObject)
        {
            // Exit puzzle and return to the main camera
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }
        }
    }

}
