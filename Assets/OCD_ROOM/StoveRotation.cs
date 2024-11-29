using UnityEngine;

public class StoveRotation : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 50f;

    void Update()
    {
        // Get input from arrow keys
        float rotationY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationY = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationY = 1f;
        }

        // Rotate the object on the y-axis
        if (rotationY != 0f)
        {
            transform.Rotate(Vector3.up * rotationY * rotationSpeed * Time.deltaTime);

            // Log current rotation
            //Debug.Log($"Object Rotation: {transform.rotation.eulerAngles}");
        }
    }
}
