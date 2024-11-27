using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PunchingBagHighlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Obj;
    private Renderer renderer;
    private Color initialColor;

    public Camera camera;

    [SerializeField] private float punchBagHealth = 100f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        initialColor = GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo)){
                if(hitInfo.collider.gameObject.tag == "punchBag"){
                    
                    punchBagHealth -= 10f;
                    Debug.Log("Health: " + punchBagHealth);
                    
                }
            }
        }

        punchBagHealth += 0.2f;

        if(punchBagHealth > 100f){
            punchBagHealth = 100f;
            Debug.Log("Punching Bag Fully Healed");
        }
        if(punchBagHealth <= 0){
            Obj.SetActive(false);
        }

        
    }

    private void OnMouseEnter()
    {
        renderer.material.color = Color.red + initialColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = initialColor;
    }
}
