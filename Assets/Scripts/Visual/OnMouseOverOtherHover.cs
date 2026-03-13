using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnMouseOverOtherHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string label;
    Coroutine holdCoroutine;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        holdCoroutine = StartCoroutine(HoldMouseOver());
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopCoroutine(holdCoroutine);
        TeamSelection.Instance.SkillHoveredOut();
    }
    IEnumerator HoldMouseOver()
    {
            yield return new WaitForSeconds(0.6f * Time.timeScale);
            StartCoroutine(TeamSelection.Instance.OtherIconHoverOver(this.gameObject, label));
    }
    private void OnDisable()
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            TeamSelection.Instance.SkillHoveredOut();
        }
    }
}
