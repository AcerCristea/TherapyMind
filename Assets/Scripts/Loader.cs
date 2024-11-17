using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public GameObject canvas;


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            canvas.SetActive(true);
        }
    }
    public void PressButton()
    {
        canvas.SetActive(false);
        LoadNextLevel();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadGame(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadGame(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
