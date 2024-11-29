using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objToSpin;
    public float rotateSpeed = 100f;
    public Vector3 right = new Vector3(0, 0, 1);
    public Vector3 left = new Vector3(0, 0, -1);

    public bool leftBool = false;
    public bool rightBool = false;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RoomManager.instance.ReturnToWall();
        }

        if (leftBool)
        {
            objToSpin.transform.Rotate(rotateSpeed * left * Time.deltaTime);
        }
        if (rightBool)
        {
            objToSpin.transform.Rotate(rotateSpeed * right * Time.deltaTime);
        }
    }

    public void rotateRight(bool right)
    {
        rightBool = right;
    }

    public void rotateLeft(bool left)
    {
        leftBool = left;
    }
}
