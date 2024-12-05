using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;

public class ButtonTextChangeHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public string hoverText = "Hovering!";

    public string hoverTextSpanish = "Hovering!";
    public string originalText;

    public string originalTextSpanish;

    void Start()
    {
        if (Language.isEnglish)
        {
            buttonText.text = originalText;
        }
        if (Language.isSpanish)
        {
            buttonText.text = originalTextSpanish;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Language.isEnglish)
        {
            buttonText.text = hoverText;
            buttonText.fontSize += 6;
            buttonText.color = Color.red;
        }
        if (Language.isSpanish)
        {
            buttonText.text = hoverTextSpanish;
            buttonText.fontSize += 6;
            buttonText.color = Color.red;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Language.isEnglish)
        {
            buttonText.text = originalText;
            buttonText.fontSize -= 6;
            buttonText.color = Color.white;
        }
        if (Language.isSpanish)
        {
            buttonText.text = originalTextSpanish;
            buttonText.fontSize -= 6;
            buttonText.color = Color.white;
        }
    }

}
