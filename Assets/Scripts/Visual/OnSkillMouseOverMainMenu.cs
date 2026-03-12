using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnSkillMouseOverMainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        TeamSelection.Instance.SkillHoveredOut();
    }
    IEnumerator HoldMouseOver()
    {
        yield return new WaitForSeconds(0.6f * Time.timeScale);
        StartCoroutine(TeamSelection.Instance.SkillHoveredOver(buttonId));
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
