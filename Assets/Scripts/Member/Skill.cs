using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject 
{
    protected List<int> reachablePositions;
    public List<int> ReachablePositions => reachablePositions;
    protected List<int> usableFromPositions;
    public List<int> UsableFromPositions => usableFromPositions;

    protected int level;
    public int Level => level;

    protected List<int> skillValuesMin;
    protected List<int> skillValuesMax;
    protected int skillValue;

    protected List<int> effectLenghts;
    protected List<int> effectValues;

    protected string skillName;
    public string SkillName => skillName;
    
    public string SkillType;
    public string AnimName;
    public bool SelfOnly;

    public virtual void UseSkill(int targetPosition)
    {
        skillValue = Random.Range(skillValuesMin[level], skillValuesMax[level] + 1);
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
}

