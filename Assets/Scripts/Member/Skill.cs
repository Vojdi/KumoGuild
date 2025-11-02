using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : ScriptableObject 
{
    protected List<int> reachablePositions;
    public List<int> ReachablePositions => reachablePositions;
    protected List<int> usableFromPositions;
    public List<int> UsableFromPositions => usableFromPositions;

    protected int level;
    public int Level => level;

    protected List<List<int>> skillValuesMin;
    protected List<List<int>> skillValuesMax;
    protected List<int> skillValues;

    protected List<List<int>> effectLenghts;
    protected List<List<int>> effectValues;

    protected Member selfMember;
    public string SkillName;
    
    public string SkillType;
    public string AnimName;
    public bool SelfOnly;
    protected List<Member> targetMembers;

    protected virtual void OnEnable()
    {
        skillValues = new List<int>();
    }
    public virtual void UseSkill(int targetPosition)
    {
        for (int s = 0; s < skillValuesMin.Count; s++)
        {
            skillValues.Add(Random.Range(skillValuesMin[s][level], skillValuesMax[s][level] + 1));
        }
        targetMembers = Target(targetPosition);
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
    public void MakeSelfOnly(int selfPos)
    {
        reachablePositions.Clear();
        reachablePositions.Add(selfPos);
    }
    List<Member> Target(int targetPosition)
    {
        List<Member> targets = new List<Member>();
        if (SkillType == "single")
        {
            foreach (var m in GameManager.Instance.Members)
            {
                if (m.Position == targetPosition)
                {
                    targets.Add(m);
                }
            }
        }
        else if (SkillType == "area")
        {
            foreach (var m in GameManager.Instance.Members)
            {
                if (reachablePositions.Contains(m.Position))
                {
                    targets.Add(m);
                }
            }
        }
        return targets;
    }
    public void SetSelfMember(Member selfM)
    {
        selfMember = selfM; 
    }
}

