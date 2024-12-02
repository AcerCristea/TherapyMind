using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaManager : MonoBehaviour
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

    /* TODO:
    - voiceline / txtbox prompts
    - go back to room on ESC
    - make the children navmesh agents
    - when player touches child & has umbrella, hands to it them
    - make rain vfx & sfx
    - color stuff
    - 
    */
    void Update()
    {
        if (PaintingTransition.hasUmbrella)
        {
            // voice / text lines
        }
        else
        {
            // prompt player to return OR nothing happens
        }
    }
}
