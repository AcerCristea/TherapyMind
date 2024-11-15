using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextChangeHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public string hoverText = "Hovering!";
    private string originalText;
    [SerializeField] private Animator canvas = null;

    void Start()
    {
        originalText = buttonText.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = hoverText;
        canvas.Play("Transition", 0, 0f);
        buttonText.fontSize = 36;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = originalText;
        canvas.Play("EndTransition", 0, 0f);
        buttonText.fontSize = 30;
    }

}
