using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShardController : MonoBehaviour
{
    public static int correctCounter = 0;
    private bool said = false;

    void Update()
    {
        if (correctCounter >= 13 && !said && !GameManager.instance.mirrorPuzzleComplete) // Adjust the value as needed
        {
            GameManager.instance.DecreaseInsanity(20f); // Adjust the value as needed
            GameManager.instance.mirrorPuzzleComplete = true; // Adjust the value as needed

            Debug.Log("mirror completed");
            said = true;
        }
    }
}
