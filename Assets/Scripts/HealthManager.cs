using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityManager : MonoBehaviour
{
    public Image insanityBar; // Reference to the UI Image for the slider bar
    public float maxInsanity = 100f; // Maximum insanity level
    public float insanityAmount = 0f; // Current insanity level


    private GameManager gameManager;



    void Start()
    {
        // Find the GameManager in the scene
        gameManager = GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void Update()
    {
        if (gameManager != null)
        {
            // Update insanity amount from GameManager
            insanityAmount = gameManager.insanityMeter;

            // Update the insanity bar fill
            insanityBar.fillAmount = insanityAmount / maxInsanity;
        }
    }

    //public void TakeDamage(float damage)
    //{
    //    healthAmount -= damage;
    //    healthBar.fillAmount = healthAmount / 100;
    //}

    //public void Heal(float healingAmount)
    //{
    //    healthAmount += healingAmount;
    //    healthAmount = Mathf.Clamp(healthAmount, 0, 100);

    //    healthBar.fillAmount = healthAmount / 100f;
    //}
}
