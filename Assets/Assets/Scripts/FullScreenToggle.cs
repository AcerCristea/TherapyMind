using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
