using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
