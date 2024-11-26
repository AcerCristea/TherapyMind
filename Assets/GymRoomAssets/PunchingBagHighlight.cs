using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBagHighlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Obj;
    private Renderer renderer;
    private Color initialColor;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        initialColor = GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        renderer.material.color = initialColor;
    }
}
