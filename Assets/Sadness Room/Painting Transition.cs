using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PaintingTransition : MonoBehaviour
{
    /*
    Bring the player to a new scene:
     - raining
    1) player starts on the outside of a "plane"
    2) walking forward, notice a kid under a tree alone
    3a) has umbrella: "that kid needs this umbrella more than me"
        3a1) player approaches kid
        3a2) Kid: "*sobbing* they're going on without me!"
            - therapist: "no they aren't. look, they're waiting for you" [hands umbrella to kid]
            - Kid: "thank you!" [runs off]
    3b) no umbrella: "i should go get an umbrella for the kid"
        - promp player to press esc to go back

    */

    public static bool hasUmbrella = false;

    void Update()
    {
        if (RoomManager.instance.activePuzzle == this.gameObject)
        {
            StartCoroutine(ToNextScene());
        }
    }

    IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Umbrella Scene");
    }
}
