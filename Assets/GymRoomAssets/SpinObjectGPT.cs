using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectGPT : MonoBehaviour
{
    public float rotationSpeed = 10f; // Adjust rotation speed
    public Vector3 rotationAxis = Vector3.up; // Define the axis of rotation (e.g., Vector3.up for y-axis)

    private bool isDragging = false; // Is the player currently dragging the valve?
    private Plane dragPlane; // Plane for calculating mouse movement
    private Vector3 previousMousePosition; // Previous mouse position in world space

    void OnMouseDown()
    {
        // Create a plane at the object's position facing the camera
        dragPlane = new Plane(Camera.main.transform.forward, transform.position);

        // Get the initial mouse position on the plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            previousMousePosition = ray.GetPoint(enter);
        }

        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        // Get the current mouse position on the plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 currentMousePosition = ray.GetPoint(enter);

            // Calculate the direction of the drag
            Vector3 dragDirection = currentMousePosition - previousMousePosition;

            // Project the drag direction onto the rotation axis
            float dragAmount = Vector3.Dot(dragDirection, transform.TransformDirection(rotationAxis));

            // Rotate the object
            transform.Rotate(rotationAxis, dragAmount * rotationSpeed, Space.Self);

            // Update the previous mouse position
            previousMousePosition = currentMousePosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
