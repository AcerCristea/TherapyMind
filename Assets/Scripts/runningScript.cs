using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class runningScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;
    public GameObject loseScreen;

    public bool winning = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(winning == true)
        {
            Debug.Log("You Won");
            loseScreen.SetActive(false);
            winScreen.SetActive(true);
        } 
        else if (winning == false)
        {
            Debug.Log("You Lost");
            loseScreen.SetActive(true);
            winScreen.SetActive(false);

        }
    }
}
