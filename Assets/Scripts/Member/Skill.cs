using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Skill : ScriptableObject
{
    public int InstanceId;
    public static int LastInstanceId;

    public List<int> ReachablePositions;
    public List<int> UsableFromPositions;

    public int Level;
    public int IconId;

    protected List<List<int>> skillValuesMin;
    protected List<List<int>> skillValuesMax;
    protected List<int> skillValues;
    protected List<bool> skillValuesSelf;

    protected List<List<int>> effectLengths;
    protected List<List<int>> effectValues;
    protected List<Type> effectTypes;
    protected List<bool> effectValuesSelf;

    public Member SelfMember;
    public string SkillName;
    protected string skillType;

    public string SkillRangeType;
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
        SetupSkillValues();
        targetMembers = Target(targetPosition);
        TargetSkill();
        SelfSkill();
    }
    void TargetSkill()
    {
        foreach (Member targetMember in targetMembers)
        {
            for (int i = 0; i < skillValuesSelf.Count; i++)
            {
                if (!skillValuesSelf[i])
                {
                    targetMember.Damage(skillValues[i], false);
                }
            }
            for (int i = 0; i < effectValuesSelf.Count; i++)
            {
                if (!effectValuesSelf[i])
                {
                    Effect eff = (Effect)Activator.CreateInstance(effectTypes[i], effectLengths[i][Level], effectValues[i][Level]);
                    eff.Attach(targetMember,InstanceId);
                }
            }
        }
    }
    public virtual void SelfSkill()
    {
        for (int i = 0; i < skillValuesSelf.Count; i++)
        {
            if (skillValuesSelf[i])
            {
                SelfMember.Damage(skillValues[i], false);
            }
        }
        for (int i = 0; i < effectValuesSelf.Count; i++)
        {
            if (effectValuesSelf[i])
            {
                Effect eff = (Effect)Activator.CreateInstance(effectTypes[i], effectLengths[i][Level], effectValues[i][Level]);
                eff.Attach(SelfMember, InstanceId);
            }
        }
    }
    void SetupSkillValues()
    {
        for (int s = 0; s < skillValuesMin.Count; s++)
        {
            skillValues.Add(UnityEngine.Random.Range(skillValuesMin[s][Level], skillValuesMax[s][Level] + 1));
        }
    }
    List<Member> Target(int targetPosition)
    {
        List<Member> targets = new List<Member>();
        if (SkillRangeType == "single")
        {
            foreach (var m in GameManager.Instance.Members)
            {
                if (m.Position == targetPosition)
                {
                    targets.Add(m);
                }
            }
        }
        else if (SkillRangeType == "multi")
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

    public static T Create<T>(Member sMember) where T : Skill
    {
        T skill = ScriptableObject.CreateInstance<T>();
        skill.SelfMember = sMember;
        skill.InstanceId = LastInstanceId++;
        return skill;
    }

    public static List<string> GetInfoForInfoBox(Skill skill, string stringType)
    {
        List<string> list = new List<string>();
        if (stringType == "name")
        {
            list.Add(skill.SkillName);
        }
        if (stringType == "skillType")
        {
            list.Add(skill.skillType);
            list.Add(skill.SkillRangeType);
        }
        if (stringType == "positions")
        {
            if (skill.SelfOnly)
            {
                list.Add("self");
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (skill.ReachablePositions.Contains(i))
                    {
                        list.Add("true");
                    }
                    else
                    {
                        list.Add("false");
                    }
                }
            }
        }
        if (stringType == "stats")
        {
            if(skill.skillValuesSelf.Count(b => b == false) > 0 || skill.effectValuesSelf.Count(b => b == false) > 0)
            {
                if(skill.SkillRangeType == "single")
                {
                    list.Add("Target:");
                }
                else
                {
                    list.Add("Targets:");
                }
            }
            for (int i = 0; i < skill.skillValuesSelf.Count; i++)
            {
                if (!skill.skillValuesSelf[i])
                {
                    if (skill.skillValuesMin[i][skill.Level] > 0)
                    {
                        list.Add($"{skill.skillValuesMin[i][skill.Level]} - {skill.skillValuesMax[i][skill.Level]} dmg");
                    }
                    else
                    {
                        list.Add($"{Math.Abs(skill.skillValuesMin[i][skill.Level])} - {Math.Abs(skill.skillValuesMax[i][skill.Level])} heal");
                    }                    
                }
            }
            for (int i = 0; i < skill.effectValuesSelf.Count; i++)
            {
                if (!skill.effectValuesSelf[i])
                {
                    Effect temp = (Effect)Activator.CreateInstance(skill.effectTypes[i], 0, 0);
                    list.Add(temp.InfoBoxSyntax(skill.effectLengths[i][skill.Level], skill.effectValues[i][skill.Level],false)); 
                }
            }

            if (skill.skillValuesSelf.Count(b => b == true) > 0 || skill.effectValuesSelf.Count(b => b == true) > 0)
            {
                list.Add("Self:");
            }
            for (int i = 0; i < skill.skillValuesSelf.Count; i++)
            {
                if (skill.skillValuesSelf[i])
                {
                    if (skill.skillValuesMin[i][skill.Level] > 0)
                    {
                        list.Add($"{skill.skillValuesMin[i][skill.Level]} - {skill.skillValuesMax[i][skill.Level]} dmg");
                    }
                    else
                    {
                        list.Add($"{Math.Abs(skill.skillValuesMin[i][skill.Level])} - {Math.Abs(skill.skillValuesMax[i][skill.Level])} heal");
                    }
                }
            }
            for (int i = 0; i < skill.effectValuesSelf.Count; i++)
            {
                if (skill.effectValuesSelf[i])
                {
                    Effect temp = (Effect)Activator.CreateInstance(skill.effectTypes[i], 0, 0);
                    list.Add(temp.InfoBoxSyntax(skill.effectLengths[i][skill.Level], skill.effectValues[i][skill.Level], false));
                }
            }

        }
        return list;
    }
}


