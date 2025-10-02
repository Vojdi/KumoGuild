
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
        Debug.Log($"{memberOnTurn.name} used {selectedSkill.SkillName}");//////
        VisualEffectManager.Instance.PlayEffectAnimation(selectedSkill, pos);
        EnableControls(false);
        Untarget();
    }
    void Untarget()
    {
        foreach (var member in GameManager.Instance.Members)
        {
            member.Targetable = false;
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
