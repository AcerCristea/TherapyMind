using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class HealthBarDetector : MonoBehaviour
{
    public GameObject dialogueMenu;
    public GameObject pauseMenu;
    public GameObject languageMenu;
    public GameObject optionsMenu;
    public GameObject resolutionMenu;
    public GameObject HealthBar;
    public void Update()
    {
        if (pauseMenu.activeSelf == true)
        {
            HealthBar.SetActive(false);
        }
        else if (dialogueMenu.activeSelf == true)
        {
            HealthBar.SetActive(false);
        }
        else if (languageMenu.activeSelf == true)
        {
            HealthBar.SetActive(false);
        }
        else if (optionsMenu.activeSelf == true)
        {
            HealthBar.SetActive(false);
        }
        else if (resolutionMenu.activeSelf == true)
        {
            HealthBar.SetActive(false);
        }
        else
        {
            HealthBar.SetActive(true);
        }
    }
}
