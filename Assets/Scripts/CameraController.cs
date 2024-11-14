using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static GameObject northCam;
    public static GameObject westCam;
    public static GameObject eastCam;
    public static GameObject southCam;

    public static int whichCam = 0;

    public GameObject[] cameraArray = {northCam, westCam, southCam, eastCam};


    // Start is called before the first frame update
    void Start()
    {
        
        updateCamera(whichCam);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            leftButton();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rightButton();
        }

    }

    private void updateCamera(int index){
        for(int i = 0; i < cameraArray.Length; i++){
            if(i == index){
                cameraArray[i].SetActive(true);
                whichCam = i;
            } else {
                if(cameraArray[i] != null){
                cameraArray[i].SetActive(false);
            }
            }
        }
    }

    public void leftButton(){
        whichCam--;
        if(whichCam < 0){
            whichCam = cameraArray.Length - 1;
        }
        updateCamera(whichCam);
    }

    public void rightButton(){
        whichCam++;
        if(whichCam > cameraArray.Length - 1){
            whichCam = 0;
        }
        updateCamera(whichCam);

    }
}
