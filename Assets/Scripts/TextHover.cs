using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;

public class ButtonTextChangeHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public string hoverText = "Hovering!";
    private string originalText;

    void Start()
    {
        originalText = buttonText.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = hoverText;
        buttonText.fontSize += 6;
        buttonText.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = originalText;
        buttonText.fontSize -= 6;
        buttonText.color = Color.black;
    }

}
