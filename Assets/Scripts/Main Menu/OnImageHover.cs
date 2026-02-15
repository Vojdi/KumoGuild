using UnityEngine;
using UnityEngine.EventSystems;


public class OnImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool special;
    RectTransform rectT;
    float startSizeX;
    float startSizeY;
    float hoverSizeX;
    float hoverSizeY;

    RectTransform rectTT;
    float startSizeXX;
    float startSizeYY;
    float hoverSizeXX;
    float hoverSizeYY;


    void Awake()
    {
        rectT = GetComponent<RectTransform>();
        startSizeY = rectT.rect.height;
        startSizeX = rectT.rect.width;

        hoverSizeY = startSizeY + 15;
        hoverSizeX = startSizeX + 15;

        if( special)
        {
            rectTT = transform.parent.GetChild(0).GetComponent<RectTransform>();
            startSizeYY = rectTT.rect.height;
            startSizeXX = rectTT.rect.width;

            hoverSizeYY = startSizeYY + 15;
            hoverSizeXX = startSizeXX + 15;
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverSizeX);
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverSizeY);

        if( special)
        {
            rectTT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hoverSizeXX);
            rectTT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hoverSizeYY);
        }
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

        if (special)
        {
            rectTT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startSizeXX);
            rectTT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, startSizeYY);
        }
    }
}
