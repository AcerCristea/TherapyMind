using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderGame : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void PressButton()
    {
        LoadNextLevel();
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
