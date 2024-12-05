using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LanguageCheck : MonoBehaviour
{
    public bool language = true;
    public GameObject endialogue;
    public GameObject encontinue;
    public GameObject spadialogue;
    public GameObject spacontinue;

    public void Update()
    {
        if (language)
        {
            Debug.Log("Spanish");
            endialogue.SetActive(false);
            encontinue.SetActive(false);
            spadialogue.SetActive(true);
            spacontinue.SetActive(true);
        }
        else
        {
            Debug.Log("English");
            spadialogue.SetActive(false);
            spacontinue.SetActive(false);
            endialogue.SetActive(true);
            encontinue.SetActive(true);
        }
    }
}
