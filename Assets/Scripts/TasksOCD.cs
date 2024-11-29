using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksOCD : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string taskName; // Unique identifier
        public GameObject taskObject; // Reference to the object
        public bool isCompleted = false; // Status of the task
        public Vector3 originalPosition; // Original position for reset
        public Quaternion originalRotation; // Original rotation for reset
    }
}
