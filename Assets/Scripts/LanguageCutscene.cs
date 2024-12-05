using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageCutscene : MonoBehaviour
{
    public GameObject english;
    public GameObject englishButton;
    public GameObject spanish;
    public GameObject spanishButton;

    public void Update()
    {
        if (Language.isEnglish)
        {
            english.SetActive(true);
            englishButton.SetActive(true);
        }
        if (Language.isSpanish)
        {
            spanish.SetActive(true);
            spanishButton.SetActive(true);
        }
    }
}
