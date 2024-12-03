using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShardController : MonoBehaviour
{
    public static int correctCounter = 0;

    void Update()
    {
        if (correctCounter >= 10)
        {
            Debug.Log("mirror completed");
        }
    }
}
