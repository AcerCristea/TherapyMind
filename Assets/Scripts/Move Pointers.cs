using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable xxxx

public class MovePointers : MonoBehaviour
{
    // cursorPointer is the pointer following the player's inputs
    public GameObject cursorPointer;
    // selectedPointer is set to stay above the item the player pressed space on 
    public GameObject selectedPointer;

    GameObject self_;

    void Start()
    {
        self_ = GetComponent<Transform>().gameObject;
        
        selectedPointer.SetActive(false);
    }

    void Update()
    {   
        // cursorPointer follows player's inputs (look at MemoriesManager->Input Handler)
        if (MemoriesManager.instance.cursor_ == self_)
        {
           cursorPointer.transform.position = self_.transform.position + new Vector3(0,4,0);
        }
        // selectedPointer stays on the player's first selection
        if (MemoriesManager.instance.selected == self_)
        {   
            selectedPointer.transform.position = self_.transform.position + new Vector3(0,4,0);
            selectedPointer.SetActive(true);
        }
        // until their selection is cleared
        else
        {
            if (!MemoriesManager.instance.selected)
            {
                
                selectedPointer.SetActive(false);
            }
        }
        
    }
        
}
