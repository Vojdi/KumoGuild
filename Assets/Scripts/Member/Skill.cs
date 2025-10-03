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
    public string AnimName;
    public bool SelfOnly;


    public virtual void UseSkill(int targetPosition)
    {

    }
    
    protected Member SingleTarget(int targetPosition)
    {
        foreach (var m in GameManager.Instance.Members)
        {
            if (m.Position == targetPosition)
            {
                return m;
            }
        }
        return null;
    }
    protected List<Member> AreaAttack(int targetPosition)
    {
        List<Member> targets = new List<Member>();
        foreach (var m in GameManager.Instance.Members)
        {
            if (reachablePositions.Contains(m.Position)){
                targets.Add(m);
            } 
        }
        return targets;
    }
    public bool SkillWorthUsingCheck(int position)
    {
        if (SelfOnly)
        {
            MakeSelfOnly(position);
        }
        if (SkillType == "single")
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (reachablePositions.Contains(m.Position))
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
    public void MakeSelfOnly(int selfPos)
    {
        reachablePositions.Clear();
        reachablePositions.Add(selfPos);
    }
}

