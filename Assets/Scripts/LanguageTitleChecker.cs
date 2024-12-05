using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageTitleChecker : MonoBehaviour
{
    public GameObject english;
    public GameObject spanish;

    public void Update()
    {
        if (Language.isEnglish)
        {
            english.SetActive(true);
            spanish.SetActive(false);
        }
        if (Language.isSpanish)
        {
            spanish.SetActive(true);
            english.SetActive(false);
        }
    }
}
