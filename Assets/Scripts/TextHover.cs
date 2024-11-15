using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = originalText;
    }
}
