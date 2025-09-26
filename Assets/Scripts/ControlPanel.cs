using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        EnableSKills();
        memberOnTurn = member;
    }
    public void SkillClicked(int index)
    {
        selectedSkill = memberOnTurn.Skills[index];

        foreach (Member member in GameManager.Instance.Members)
        {
            if (selectedSkill.ReachablePositions.Contains(member.Position))
            {
                member.Targetable = true;
            }
            else
            {
                member.Targetable = false;
            }
        }
    }
    public void SkillPositionSelected(int pos)
    {
        selectedSkill.UseSkill(pos);
        Untarget();
    }
    void Untarget()
    {
        foreach (var member in GameManager.Instance.Members)
        {
            member.Targetable = false;
        }
    }
    void EnableSKills()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
