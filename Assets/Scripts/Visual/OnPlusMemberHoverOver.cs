using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class OnPlusMemberHoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int buttonId;
    Sprite originSprite;
    Coroutine holdCoroutine;

    void Start()
    {
        originSprite = GetComponent<Image>().sprite;
    }
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
        if (GetComponent<Image>().sprite == originSprite) {
            yield return new WaitForSeconds(0.6f * Time.timeScale);
            StartCoroutine(TeamSelection.Instance.PlusMemberHoverOverPlus(buttonId));
        }
        else
        {
            yield return new WaitForSeconds(0.6f * Time.timeScale);
            StartCoroutine(TeamSelection.Instance.PlusMemberHoverOverMember(buttonId));
        }
            
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
