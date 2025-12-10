using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSkillMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int buttonId;
    Coroutine holdCoroutine;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        holdCoroutine = StartCoroutine(HoldMouseOver());
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    { 
        StopCoroutine(holdCoroutine);
        ControlPanel.Instance.SkillHoveredOut();
    }
    IEnumerator HoldMouseOver()
    {
        yield return new WaitForSeconds(0.6f * Time.timeScale);
        StartCoroutine(ControlPanel.Instance.SkillHoveredOver(buttonId));
    }
}
