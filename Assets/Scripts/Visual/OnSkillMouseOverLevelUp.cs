using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnSkillMouseOverLevelUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int buttonId;
    Coroutine holdCoroutine;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        holdCoroutine = StartCoroutine(HoldMouseOver());
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            LevelUpPanel.Instance.SkillHoveredOut();
        }
    }
    IEnumerator HoldMouseOver()
    {

        if(!LevelUpPanel.Instance.CheckIfUpgraded(buttonId))
        {
            yield return new WaitForSeconds(0.6f * Time.timeScale);
            StartCoroutine(LevelUpPanel.Instance.SkillHoveredOver(buttonId));
        }
    }
    private void OnDisable()
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            ControlPanel.Instance.SkillHoveredOut();
        }
    }
}
