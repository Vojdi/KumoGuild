using UnityEngine;
using UnityEngine.EventSystems;


public class OnImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rectT;
    float startSizeX;
    float startSizeY;
    float hoverSizeX;
    float hoverSizeY;
    void Awake()
    {
        rectT = GetComponent<RectTransform>();
        startSizeY = rectT.rect.height;
        startSizeX = rectT.rect.width;

        hoverSizeY = startSizeY + 15;
        hoverSizeX = startSizeX + 15;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverSizeX);
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverSizeY);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Exit();
    }
    private void OnDisable()
    {
        Exit();   
    }
    void Exit()
    {
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startSizeX);
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, startSizeY);
    }
}
