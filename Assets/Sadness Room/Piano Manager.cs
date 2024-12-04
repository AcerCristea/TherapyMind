using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
For this "puzzle", I'm treating it like a rhythm game where the
player has to rely on musical notation (all info provided).
* The "beats" are invisble cubes flying into the keyboard
    * controlled by TrackStart
    * the track only starts moving when the player has played the first note
* The "bar" is the keyboard keys 
    * if a beat flies past the key, the track is reset
    * if a beat is played while inside the key, the beat is counted
*/

public class PianoManager : MonoBehaviour
{
    public static PianoManager instance;
    
    public GameObject track;
    public Vector3 trackStartPosition;
    public bool trackIsMoving;
    public bool metroIsPlaying;
    public GameObject correctNote;

    void Awake()
    {
        instance = this;
        trackStartPosition = track.transform.position;
    }

    void Update()
    {
        if (RoomManager.instance.activePuzzle == this.gameObject)
        {
            metroIsPlaying = true;
            if (Input.GetMouseButton(0))
            {
                PressKey();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RoomManager.instance.ReturnToWall();
            }
        }
    }

    // check that the the player clicks on a key then plays the note assigned to the key
    public void PressKey()
    {
        Ray ray = RoomManager.instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            KeyController hitKeyController = hit.collider.gameObject.GetComponent<KeyController>();
            if (hitKeyController != null)
            {
                hitKeyController.PlayNote();
                // chekc if the note is the right one otherwise reset
                if (hitKeyController.gameObject != correctNote)
                {
                    track.GetComponent<TrackController>().ResetTrack();
                }

                // THIS IS WHERE COMPLETION IS CHECKED
                if (track.GetComponent<TrackController>().isEmpty())
                {
                    StartCoroutine(StartMusic());
                }
            }
        }
    }

    IEnumerator StartMusic()
    {
        GameManager.instance.DecreaseInsanity(20f); // Adjust the value as needed
        GameManager.instance.pianoPuzzleComplete = true; // Mark the puzzle as complete

        Debug.Log("CONGRATS");
        yield return new WaitForSeconds(3);
        GetComponent<AudioSource>().Play();
    }
    
}
