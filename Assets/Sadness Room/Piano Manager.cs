using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PianoManager : MonoBehaviour
{
    public static PianoManager instance;
    AudioSource cNote;

    void Awake()
    {
        instance = this;
        cNote = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Debug.Log("current active cam = " + Camera.current.name);
        if (RoomManager.instance.activePuzzle == this.gameObject)
        {
            // use mouse to press a key
            if (Input.GetMouseButton(0))
            {
                PlayNote();
            }
            // use escape to exit puzzle
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RoomManager.instance.ReturnToWall();
            }
        }
    }

    public void PlayNote()
    {
        Ray ray = RoomManager.instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<KeyController>() != null)
            {
                hit.collider.gameObject.GetComponent<KeyController>().PressKey();
            }
        }
    }
}
