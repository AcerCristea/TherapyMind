using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed = 100f;
    public Vector3 right = new Vector3(0, 0, 1);
    public Vector3 left = new Vector3(0, 0, -1);

    public bool leftBool = false;
    public bool rightBool = false;

    void Update()
    {
        if (leftBool)
        {
            transform.Rotate(rotateSpeed * left * Time.deltaTime);
        }
        if (rightBool)
        {
            transform.Rotate(rotateSpeed * right * Time.deltaTime);
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

    public void debugStatement()
    {
        Debug.Log("I'm in the button");
    }
}
