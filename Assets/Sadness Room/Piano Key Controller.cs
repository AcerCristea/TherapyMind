using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    AudioSource audioSource;
    public bool played;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        played = false;
    }

    // play any note clicked
    public void PlayNote()
    {
        played = true;
        audioSource.Play();
    }

    // player plays the correct note on time
    private void OnTriggerStay(Collider collider_)
    {
        if (collider_.gameObject.CompareTag("Target"))
        {
            PianoManager.instance.correctNote = this.gameObject;
            // disable the track-note if its played
            if (played)
            {
                StartCoroutine(ResetKey(collider_)); // need some delay cuz it wouldnt work without it
                collider_.gameObject.SetActive(false);
                PianoManager.instance.trackIsMoving = true; // starts the track  
            }
        }
    }

    // if the player misses a note, it isn't counted and the player has to restart
    private void OnTriggerExit(Collider collider_)
    {
        if (collider_.gameObject.CompareTag("Target") && !played)
        {
            // reset every key
            foreach(Transform key in this.transform.parent)
            {
                key.gameObject.GetComponent<KeyController>().played = false;
            }
            // reset track
            PianoManager.instance.track.GetComponent<TrackController>().ResetTrack();
        }
    }

    /* 
    KeyController.played needs to be reset. without it, if a note that's 
    already been played reappears on the track, it is automatically counted
    */
    IEnumerator ResetKey(Collider collider_)
    {
        collider_.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        played = false;
    }
}
