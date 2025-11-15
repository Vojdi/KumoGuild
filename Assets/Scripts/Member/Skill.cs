using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Skill : ScriptableObject 
{
    public List<int> ReachablePositions;
    public List<int> UsableFromPositions;

    
    public int Level;

    protected List<List<int>> skillValuesMin;
    protected List<List<int>> skillValuesMax;
    protected List<int> skillValues;

    protected List<List<int>> effectLenghts;
    protected List<List<int>> effectValues;

    public Member SelfMember;
    public string SkillName;
    
    public string SkillType;
    public string AnimName;
    public bool SelfOnly;
    public bool HasSelfSkill;
    protected List<Member> targetMembers;

    protected virtual void OnEnable()
    {
        skillValues = new List<int>();
    }
    public virtual void UseSkill(int targetPosition)
    {
        for (int s = 0; s < skillValuesMin.Count; s++)
        {
            skillValues.Add(Random.Range(skillValuesMin[s][Level], skillValuesMax[s][Level] + 1));
        }
        targetMembers = Target(targetPosition);
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
                if (ReachablePositions.Contains(m.Position))
                {
                    targets.Add(m);
                }
            }
        }
        return targets;
    }
    public virtual void SelfUseSkill()
    {

    }
    public static T Create<T>(Member sMember) where T : Skill
    {
        T skill = ScriptableObject.CreateInstance<T>();
        skill.SelfMember = sMember;
        return skill;
    }
}

