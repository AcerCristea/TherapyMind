using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaunaFunction : MonoBehaviour
{

    public GameObject leftSpin;
    public GameObject middleSpin;
    public GameObject rightSpin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftSpin.transform.localRotation.eulerAngles.z >= 5){
            Debug.Log("-100 Degrees");
        }
    }
}
