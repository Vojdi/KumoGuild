
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text memberNameText;
    [SerializeField] Image memberIcon;
    static ControlPanel instance;
    public static ControlPanel Instance => instance;
    AllyMember memberOnTurn;
    Skill selectedSkill;
    private void Awake()
    {
        instance = this;
    }
    public void AllyTurn(AllyMember member)
    {
        memberOnTurn = member;
        StartCoroutine(EnableControls(true));
    }
    public void SkillClicked(int index)
    {
        selectedSkill = memberOnTurn.Skills[index];
        if (selectedSkill.SelfOnly)
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (selectedSkill.SelfMember.Position ==  m.Position)
                {
                    m.Targetable = true;
                }
                else
                {
                    m.Targetable = false;
                }
            }
        }
        else
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (m.Effects.OfType<TauntEffect>().Any() && m is EnemyMember)
                {
                    if (selectedSkill.ReachablePositions.Contains(m.Position))
                    {
                        Untarget();
                        m.Targetable = true;
                        break;
                    }
                }
                if (selectedSkill.ReachablePositions.Contains(m.Position))
                {
                    m.Targetable = true;
                }
                else
                {
                    m.Targetable = false;
                }
            }
        }
        VisualEffectManager.Instance.TargetArrows();
    }
    public void SkillPositionSelected(int pos)
    {
        Debug.Log($"{memberOnTurn} used {selectedSkill.SkillName}");//////
        VisualEffectManager.Instance.PlayEffectAnimation(selectedSkill, pos);
        StartCoroutine(EnableControls(false));
        Untarget();
    }
    void Untarget()
    {
        foreach (var m in GameManager.Instance.Members)
        {
            m.Targetable = false;
        }
        VisualEffectManager.Instance.TargetArrows();
    }
    IEnumerator EnableControls(bool parameter)
    {
        yield return null;
        if (parameter == true)
        {
            memberNameText.text = memberOnTurn.MemberName;
            memberIcon.sprite = ImageManager.Instance.GetSprite(memberOnTurn.IconId);
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(parameter);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(memberNameText.rectTransform);


    }
}
