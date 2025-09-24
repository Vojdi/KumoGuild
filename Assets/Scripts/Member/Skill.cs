using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject 
{
    protected List<int> reachablePositions;
    public List<int> ReachablePositions => reachablePositions;

    protected List<int> usableFromPositions;
    public List<int> UsableFromPositions => usableFromPositions;

    protected int skillValue;
    public int SkillValue => skillValue;
    protected string skillName;
    public string SkillName => skillName;

    public string SkillType;

    public virtual void UseSkill(int targetPosition)
    {

    }
    
    protected Member SingleTarget(int targetPosition)
    {
        foreach (var member in GameManager.Instance.Members)
        {
            if (member.Position == targetPosition)
            {
                return member;
            }
        }
        return null;
    }
    protected List<Member> AreaAttack(int targetPosition)
    {
        List<Member> targets = new List<Member>();
        foreach (var member in GameManager.Instance.Members)
        {
            if (reachablePositions.Contains(member.Position)){
                targets.Add(member);
            } 
        }
        return targets;
    }
    public bool SkillWorthUsingCheck()
    {
        if (SkillType == "single")
        {
            foreach (Member member in GameManager.Instance.Members)
            {
                Debug.Log(member.Position + "member Pos");//////
                if (reachablePositions.Contains(member.Position))
                {
                    return true;
                }
            }
            return false;
        }
        else if (SkillType == "area")
        {
            List<int> memberPositions = new List<int>();
            foreach (Member m in GameManager.Instance.Members)
            {
                memberPositions.Add(m.Position);
            }

            foreach (int pos in reachablePositions)
            {
                if (!memberPositions.Contains(pos))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}

