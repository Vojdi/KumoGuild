using UnityEngine;
using UnityEngine.EventSystems;

public class OnTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TMPro.TMP_Text text;
    float startSize;
    float hoverSize;
    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        startSize = text.fontSize;
        hoverSize = startSize + 10;

    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        text.fontSize = hoverSize;
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        text.fontSize = startSize;
    }
    public void SetToStartSize()
    {
        text.fontSize = startSize;
    }
}
