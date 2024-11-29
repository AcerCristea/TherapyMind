using System.Collections.Generic;
using UnityEngine;
using static TasksOCD;

public class GameManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public float timeLimit = 30f;
    private float timer;
    private bool levelCompleted = false;

    void Start()
    {
        // Save original positions and reset timer
        foreach (var task in tasks)
        {
            task.originalPosition = task.taskObject.transform.position;
            task.originalRotation = task.taskObject.transform.rotation;
        }
        timer = timeLimit;

    }

    void Update()
    {
        if (!levelCompleted)
        {
            // Decrease the timer
            timer -= Time.deltaTime;

            // Check if all tasks are complete
            if (AreAllTasksCompleted())
            {
                CompleteLevel();
            }
            else if (timer <= 0f)
            {
                // Reset objects and restart
                ResetTasks();
            }
        }

    }

    bool AreAllTasksCompleted()
    {
        foreach (var task in tasks)
        {
            if (!task.isCompleted)
                return false;
        }
        return true;
    }

    void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Level Completed!");
        // Add level completion logic here
    }

    void ResetTasks()
    {
        Debug.Log("Time's up! Resetting tasks...");
        foreach (var task in tasks)
        {
            task.taskObject.transform.position = task.originalPosition;
            task.taskObject.transform.rotation = task.originalRotation;
            task.isCompleted = false;
        }
        timer = timeLimit;
    }

    public void MarkTaskAsComplete(string taskName)
    {
        foreach (var task in tasks)
        {
            if (task.taskName == taskName)
            {
                task.isCompleted = true;
                Debug.Log($"Task {taskName} completed!");
                return;
            }
        }
    }

    public void MarkTaskAsIncomplete(string taskName)
    {
        foreach (var task in tasks)
        {
            if (task.taskName == taskName)
            {
                task.isCompleted = false;
                Debug.Log($"Task {taskName} completed!");
                return;
            }
        }
    }
}

