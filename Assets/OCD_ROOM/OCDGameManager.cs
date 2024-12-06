using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TestTools;
using static TasksOCD;
using UnityEngine.SceneManagement;

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

    public float insanityMeter = 0f;
    public float maxInsanity = 100f;
    // public float insanityRate = 1f; // How fast insanity increases per second
    public float insanityRate = 3f;
    public float health = 100f; // Player's health
    public float healthReductionRate = 5f; // How fast health decreases when insanity is maxed

    public bool pianoPuzzleComplete = false;
    public bool memoryPuzzleComplete = false;
    public bool mirrorPuzzleComplete = false;
    public bool valvePuzzleComplete = false;
    public bool PBPuzzleComplete = false;

    public AudioSource heartbeatAudio; // Heartbeat AudioSource
    public float minHeartbeatVolume = 0.1f;
    public float maxHeartbeatVolume = 1f;
    public float minHeartbeatPitch = 0.5f;
    public float maxHeartbeatPitch = 2f;

    public GameObject MenuDialogue;

    [Header("Post Processing")]
    public bool startPP = false;
    public GameObject ppObj;
    public PostProcessVolume ppVolume;

    public static AudioSource Complete;

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
        // post processing
        ppVolume = ppObj.GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        if (!MenuDialogue.activeSelf) // Checks if MenuDialogue is NOT active
        {

            UpdateHeartbeat();

            IncreaseInsanity(Time.deltaTime * insanityRate);

            CheckGameCompletion();

            // Reduce health if insanity is maxed (universal)
            if (insanityMeter >= maxInsanity)
            {
                ReduceHealth(Time.deltaTime * healthReductionRate);
            }
            // start increasing vfx after insanity reaches a threshold
            startPP = (insanityMeter >= maxInsanity*0.66f) ? true : false;


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

    }
    void UpdateHeartbeat()
    {
        if (heartbeatAudio != null)
        {
            // Calculate volume and pitch based on insanity
            float normalizedInsanity = insanityMeter / maxInsanity;
            heartbeatAudio.volume = Mathf.Lerp(minHeartbeatVolume, maxHeartbeatVolume, normalizedInsanity);
            heartbeatAudio.pitch = Mathf.Lerp(minHeartbeatPitch, maxHeartbeatPitch, normalizedInsanity);
        }
    }


    void IncreaseInsanity(float amount)
    {
        insanityMeter = Mathf.Clamp(insanityMeter + amount, 0, maxInsanity);
        if (startPP && ppVolume.weight < 1)
        {
            ppVolume.weight += 0.05f * Time.deltaTime;
        }

        // Debug.Log($"Insanity: {insanityMeter}/{maxInsanity}");
    }

    void ReduceHealth(float amount)
    {
        health = Mathf.Max(health - amount, 0);
        Debug.Log($"Health: {health}");
        if (health <= 0)
        {
            Debug.Log("Game Over! Player has lost all health.");
            // Add game over logic here
            SceneManager.LoadScene("GameOver");
        }
        
    }

    public void DecreaseInsanity(float amount)
    {
        insanityMeter = Mathf.Clamp(insanityMeter - amount, 0, maxInsanity);
        if (startPP && ppVolume.weight > 0.15f)
        {
            ppVolume.weight -= 0.15f;
        }
        Debug.Log($"Insanity decreased: {insanityMeter}/{maxInsanity}");
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

        DecreaseInsanity(20f);

        foreach (var task in tasks)
        {

            if (task.taskName == "Burner")
            {
                isBurnerTaskComplete = true; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isBurnerTaskComplete);
                //Complete.Play();
            }
            if (task.taskName == "Faucet")
            {
                isFaucetTaskComplete = true; // Shared bool for burner task completion
                Debug.Log("CHEECKKK: " + isFaucetTaskComplete);
                //Complete.Play();

            }



            if (task.taskName == taskName)
            {
                task.isCompleted = true;
                Debug.Log($"Task {taskName} completed!");
                //Complete.Play();
                return;
            }
        }
    }

    public void MarkTaskAsIncomplete(string taskName)
    {
        IncreaseInsanity(20f);


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

    void CheckGameCompletion()
    {

        //    if (memoryPuzzleComplete)
        //    {
        //        Complete.Play();
        //    }

        //    if (pianoPuzzleComplete)
        //    {
        //        Complete.Play();
        //    }

        //    if (valvePuzzleComplete)
        //    {
        //        Complete.Play();
        //    }
        //    if (PBPuzzleComplete)
        //    {
        //        Complete.Play();
        //    }

        //    if (mirrorPuzzleComplete)
        //    {
        //        Complete.Play();
        //    }

        //if (isBurnerTaskComplete)
        //{
        //    Complete.Play();
        //}

        //if (isFaucetTaskComplete)
        //{
        //    Complete.Play();
        //}

        if (AreAllTasksCompleted() && memoryPuzzleComplete && pianoPuzzleComplete && valvePuzzleComplete && PBPuzzleComplete && mirrorPuzzleComplete)
        {
            Debug.Log("Game Completed! Congratulations!");
            SceneManager.LoadScene("WinnerScene");
            //Add logic for game completion, such as showing a victory screen
        }
    }
}