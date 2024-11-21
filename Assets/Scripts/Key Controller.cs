using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KeyController : MonoBehaviour
{
    AudioSource noteOfKey;
    
    void Start ()
    {
        noteOfKey = GetComponent<AudioSource>();
    }

    public void PressKey()
    {
            noteOfKey.Play();
    }
}
