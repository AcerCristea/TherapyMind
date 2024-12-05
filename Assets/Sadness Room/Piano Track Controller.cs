using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public float speed = 0.4f;

    void Update()
    {
        // only starts moving after the player has clicked on a key
        if (PianoManager.instance.trackIsMoving)
        {
            GetComponent<Transform>().position += -transform.forward * Time.deltaTime * speed;
        }
    }

    public bool isEmpty()
    {
        foreach (Transform child in GetComponent<Transform>())
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void ResetTrack()
    {
        Debug.Log("track has been reset to " + PianoManager.instance.trackStartPosition);
        // reset track
        this.transform.position = PianoManager.instance.trackStartPosition;
        foreach(Transform note in this.transform)
        {
            note.gameObject.SetActive(true);
        }
        // pause track again until the player plays the first note
        PianoManager.instance.trackIsMoving = false;
    }
}

