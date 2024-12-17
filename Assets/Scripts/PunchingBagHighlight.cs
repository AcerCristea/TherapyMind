using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PunchingBagManager : MonoBehaviour
{
    public static PunchingBagManager instance;

    private Renderer renderer1;
    private Color initialColor;
    private Collider punchBagCollider;
    private Color punchColor;
    private Color highlightColor;

    [SerializeField] private float punchBagHealth = 100f;
    [SerializeField] private RoomManager roomManager; // Reference to a copy of RoomManager
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        instance = this;
        renderer1 = GetComponent<Renderer>();
        initialColor = GetComponent<Renderer>().material.GetColor("_Color");

        punchBagCollider = GetComponent<Collider>();

        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<RoomManager>();
        gameManager = FindFirstObjectByType<GameManager>();

        punchColor = new Color(0.8f, 0f, 0f, 1f);
        highlightColor = new Color(1f, .58f, .58f, 1f);

    }

    void Update()
    {
        if (roomManager.activePuzzle == this.transform.parent.gameObject) // Adjust the value as needed
        {
            // click to punch bag
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = roomManager.activeCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.tag == "Puzzle")
                    {
                        changeColor(punchColor);
                        punchBagHealth -= 10f;
                        Debug.Log("Health: " + punchBagHealth);

                    }
                }
            }
            // escape to quit
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                roomManager.ReturnToWall();
            }

            // bag constantly heals
            punchBagHealth += 0.01f;
            // cap health
            if (punchBagHealth > 100f)
            {
                punchBagHealth = 100f;
            }
            // bag breaks
            if (punchBagHealth <= 0 && !GameManager.instance.PBPuzzleComplete)
            {
                changeColor(Color.black);
                punchBagCollider.enabled = false;
                //this.gameObject.SetActive(false);
                Debug.Log("PUNCH BAG DONE, checked in PunchingBagHighlight");
                GameManager.instance.DecreaseInsanity(20f); // Adjust the value as needed
                GameManager.instance.PBPuzzleComplete = true; // Adjust the value as needed
            }
        }
    }

    private void OnMouseUp()
    {
        changeColor(highlightColor);
    }

    // highlight on mouse hovering
    private void OnMouseEnter()
    {
        changeColor(highlightColor);
    }
    private void OnMouseExit()
    {
        changeColor(initialColor);
    }

    private void changeColor(Color theColor)
    {
        renderer1.material.color = theColor;
    }

}
