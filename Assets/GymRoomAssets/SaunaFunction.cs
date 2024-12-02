using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaunaFunction : MonoBehaviour
{

    public GameObject leftSpin;
    public GameObject middleSpin;
    public GameObject rightSpin;

    public GameObject saunaDoor;

    private bool leftGreen = false;
    private bool middleGreen = false;
    private bool rightGreen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftSpinRotation = leftSpin.transform.localRotation.eulerAngles.z;
        float middleSpinRotation = middleSpin.transform.localRotation.eulerAngles.z;
        float rightSpinRotation = rightSpin.transform.localRotation.eulerAngles.z;

        

        if(leftGreen && middleGreen && rightGreen){
            saunaDoor.SetActive(false);
            
        }
        //Debug.Log(leftSpinRotation);
        else if(leftSpinRotation >= 268 && leftSpinRotation <= 274){
            if(!leftGreen){
                Debug.Log("LeftGreen Check");
            }
            
            leftGreen = true;
            

        }
        else if(middleSpinRotation >= 312 && middleSpinRotation <= 318){
            if(!middleGreen){
                Debug.Log("MiddleGreen Check");
            }
            middleGreen = true;
        }
        else if(rightSpinRotation >= 52 && rightSpinRotation <= 58){
            
            if(!rightGreen){
                Debug.Log("RightGreen Check");
            }
            
            rightGreen = true;
            
        }

    }
}
