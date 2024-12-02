using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PunchingBagManager : MonoBehaviour
{
    public static PunchingBagManager instance; 

    private Renderer renderer1;
    private Color initialColor;

    [SerializeField] private float punchBagHealth = 100f;
    [SerializeField] private AngerRoomManager roomManager; // Reference to a copy of RoomManager

    void Start()
    {
        instance = this;
        renderer1 = GetComponent<Renderer>();
        initialColor = GetComponent<Renderer>().material.GetColor("_Color");

        // Find the RoomManager in the scene
        roomManager = FindFirstObjectByType<AngerRoomManager>();
    }

    void Update()
    {
        if (roomManager.activePuzzle == this.transform.parent.gameObject)
        {
            // click to punch bag
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = roomManager.activeCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.tag == "Puzzle")
                    {

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
            punchBagHealth += 0.1f;
            // cap health
            if (punchBagHealth > 100f)
            {
                punchBagHealth = 100f;
            }
            // bag breaks
            if (punchBagHealth <= 0)
            {
                this.gameObject.SetActive(false);
                Debug.Log("PUNCH BAG DONE, checked in PunchingBagHighlight");
            }
        }
    }

    // highlight on mouse hovering
    private void OnMouseEnter()
    {
        renderer1.material.color = Color.red + initialColor;
    }
    private void OnMouseExit()
    {
        renderer1.material.color = initialColor;
    }
}
