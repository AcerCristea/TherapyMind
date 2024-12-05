using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/* IMPORTANT
Make sure the hierarchy reflects the order of the objects.
At the top should be the left-most obj & the bottom should
be the right-most obj.
*/

/* player...
 * cycles through bottles using A/D
 * selects first bottle using SPACE and can deselect by
   pressing it again
 * selects the second bottle to swap with using SPACE
 * when both are selected, automatically swap
*/

public class MemoriesManager : MonoBehaviour
{
    // need a manager/singleton/thing to keep track of some vars
    public static MemoriesManager instance;

    #region Variables
    public GameObject cursor_; // shows what the player is hovering over
    public GameObject selected; // shows the 1st obj the player wants to swap
    public GameObject to_swap; // the 2nd obj the player wants to swap
    public List<GameObject> memories; // backend representation of the item order
    public List<GameObject> correctOrder; // representation of the correct order of items
    private bool puzzleCompleted = false;
    [SerializeField] private GameManager gameManager;
    #endregion

    #region Initialize Stuff
    void Awake()
    {
        instance = this;
        memories.Capacity = GetComponent<Transform>().childCount - 3; // subtracting one to account for the pointer obj
        selected = null;
        to_swap = null;
    }

    void Start()
    {
        for (int i = 0; i < memories.Capacity; i++)
        {
            memories.Add(GetComponent<Transform>().GetChild(i).gameObject);
        }
        cursor_ = memories[0];

        gameManager = FindFirstObjectByType<GameManager>();
    }
    #endregion

    #region Input Handling
    void Update()
    {
        // only operate when the player clicks this puzzle
        if (RoomManager.instance.activePuzzle == this.gameObject)
        {
            // highlight obj to the left on A
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
            // highlight obj to the right on D
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
            // toggle selection of highlighted obj on SPACE
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToggleSelect();
            }
            // exit puzzle
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RoomManager.instance.ReturnToWall();
            }
            // swap objs automatically
            if (to_swap != null)
            {
                SwapObjs();
                cursor_ = to_swap;
                to_swap = null;
                selected = null;
            }
            // check if puzzle is complete

            if (CompareList(memories, correctOrder) && (puzzleCompleted == false))
            {

                GameManager.instance.DecreaseInsanity(20f); // Adjust the value as needed
                GameManager.instance.memoryPuzzleComplete = true; // Adjust the value as needed
                puzzleCompleted = true;
                Debug.Log("You did it! Yippee!");
            }
        }
    }
    #endregion

    #region Methods
    /* MoveRight() & MoveLeft()
    Description:
    * update cursor_ when moving left/right along the list
    Logic:
    * if you want to move right from the right-most element, return to start
    * if you want to move left from the left-most, skip to back
    */
    public void MoveRight()
    {
        if (memories.IndexOf(cursor_) == memories.Capacity - 1)
        {
            cursor_ = memories[0];
        }
        else
        {
            cursor_ = memories[memories.IndexOf(cursor_) + 1];
        }
        return;
    }
    public void MoveLeft()
    {
        if (memories.IndexOf(cursor_) == 0)
        {
            cursor_ = memories[memories.Capacity - 1];
        }
        else
        {
            cursor_ = memories[memories.IndexOf(cursor_) - 1];
        }
    }

    /* ToggleSelect()
    Description: 
    * using space to select the first element to swap
    Logic:
    * if space is pressed again on that same element, deselect it
    * if space is used on another element, that element is chosen to be
      swapped with the first selected element immediately
    */
    public void ToggleSelect()
    {
        if (selected == cursor_) // cancel selection
        {
            selected = null;
        }
        else // decide if to_swap or selected will be selected 
        {
            if (selected)
            {
                to_swap = cursor_;
            }
            else
            {
                selected = cursor_;
            }
        }
    }

    /* SwapObjs
    * it's not that hard to read
    */
    public void SwapObjs()
    {
        // swap the two selected items in-game
        Vector3 temp = selected.transform.position;
        selected.transform.position = to_swap.transform.position;
        to_swap.transform.position = temp;
        // swap them in backend
        int indexOfToSwap = memories.IndexOf(to_swap);
        memories[memories.IndexOf(selected)] = to_swap;
        memories[indexOfToSwap] = selected;
    }
    #endregion

    /*
    cuz .Equals() wasn't working
    */
    public bool CompareList(List<GameObject> input1, List<GameObject> input2)
    {
        for (int i = 0; i < input1.Capacity; i++)
        {
            if (input1[i] != input2[i])
            {
                return false;
            }
        }
        return true;
    }
}