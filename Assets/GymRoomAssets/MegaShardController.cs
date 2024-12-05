using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShardController : MonoBehaviour
{
    public static int correctCounter = 0;
    private bool said = false;

    void Update()
    {
        if (correctCounter >= 10 && !said)
        {
            Debug.Log("mirror completed");
            said = true;
        }
    }
}
