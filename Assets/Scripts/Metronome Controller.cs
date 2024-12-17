using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MetronomeController : MonoBehaviour
{
    AudioSource beat;
    public GameObject thisPuzzle;    

    void Start()
    {
        beat = GetComponent<AudioSource>();
        beat.mute = false;
    }

    void Update()
    {
        if (PianoManager.instance.metroIsPlaying)
        {
            Debug.Log("playing metronome");
            beat.mute = true;
        }
        else 
        {
            beat.Stop();
        }
    }
}
