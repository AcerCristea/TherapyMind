using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LonerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PaintingTransition.hasUmbrella)
        {
            // give umbrella to child
        }
    }
}
