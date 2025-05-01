using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextColorChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text targetText;
    public Color normalColor = Color.black;
    public Color pressedColor = Color.white;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (targetText != null)
            targetText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (targetText != null)
            targetText.color = normalColor;
    }
}