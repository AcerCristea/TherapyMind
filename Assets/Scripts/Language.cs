using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static bool isEnglish;
    public static bool isSpanish;

    public static Language instance;

    public void changeEnglish()
    {
        Debug.Log("English");
        isEnglish = true;
        isSpanish = false;
    }

    public void changeSpanish()
    {
        Debug.Log("Spanish");
        isEnglish = false;
        isSpanish = true;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
