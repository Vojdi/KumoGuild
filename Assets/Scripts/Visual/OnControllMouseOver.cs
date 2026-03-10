using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnControllMouseOver : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] string title;
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
        StartCoroutine(ControlPanel.Instance.ControlHoveredOver(this.gameObject, title));
    }
    private void OnDisable()
    {
        if(holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            ControlPanel.Instance.SkillHoveredOut();
        }
    }
}
