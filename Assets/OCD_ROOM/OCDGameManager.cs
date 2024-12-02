using System.Collections.Generic;
using UnityEngine;
using static TasksOCD;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Add this static reference
    public List<Task> tasks = new List<Task>();
    public float timeLimit = 30f;
    private float timer;
    private bool levelCompleted = false;
    public GameObject flameEffect;  // Public reference to the flame effect GameObject
    public GameObject water;
    public GameObject waterSink;
    public AudioSource runningWaterAudio;
    public AudioSource stoveAudio;
    private bool isTimerActive = true;  // Flag to control whether the timer is active

    public bool isBurnerTaskComplete = false; // Shared bool for burner task completion
    public bool isFaucetTaskComplete = false; // Shared bool for burner task completion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (isTimerActive && !levelCompleted)
        {
            // Decrease the timer only if it is active
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

        water.SetActive(true);
        waterSink.SetActive(true);
        flameEffect.SetActive(true);
        runningWaterAudio.Play();
        stoveAudio.Play();


    timer = timeLimit;
    }

    public void MarkTaskAsComplete(string taskName)
    {
        foreach (var task in tasks)
        {


            if (task.taskName == "Burner")
            {
                isBurnerTaskComplete = true; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isBurnerTaskComplete);
            }
            if (task.taskName == "Faucet")
            {
                isFaucetTaskComplete = true; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isFaucetTaskComplete);

            }

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

            if (task.taskName == "Burner")
            {
                isBurnerTaskComplete = false; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isBurnerTaskComplete);
            }
            if (task.taskName == "Faucet")
            {
                isFaucetTaskComplete = false; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isFaucetTaskComplete);

            }


            if (task.taskName == taskName)
            {
                task.isCompleted = false;
                Debug.Log($"Task {taskName} completed!");
                return;
            }
        }
    }

    // Stops the timer
    public void StopTimer()
    {
        // Optionally you can stop the timer and store the remaining time
        isTimerActive = false;
    }

    // Resets and starts the timer
    public void ResetAndStartTimer()
    {
        isTimerActive = true;
        timer = timeLimit;
        levelCompleted = false;
    }
}

