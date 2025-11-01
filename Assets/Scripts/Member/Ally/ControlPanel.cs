
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
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
        EnableControls(true);
        memberOnTurn = member;
    }
    public void SkillClicked(int index)
    {
        selectedSkill = memberOnTurn.Skills[index];
        if (selectedSkill.SelfOnly)
        {
            selectedSkill.MakeSelfOnly(memberOnTurn.Position);
        }
        foreach (Member m in GameManager.Instance.Members)
        {
            if(m.Effects.OfType<TauntEffect>().Any() && m is EnemyMember)
            {
                if (selectedSkill.ReachablePositions.Contains(m.Position)) {
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
    public void SkillPositionSelected(int pos)
    {
        Debug.Log($"{memberOnTurn} used {selectedSkill.SkillName}");//////
        VisualEffectManager.Instance.PlayEffectAnimation(selectedSkill, pos);
        EnableControls(false);
        Untarget();
    }
    void Untarget()
    {
        foreach (var m in GameManager.Instance.Members)
        {
            m.Targetable = false;
        }
    }
    void EnableControls(bool parameter)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(parameter);
        }
    }
}
