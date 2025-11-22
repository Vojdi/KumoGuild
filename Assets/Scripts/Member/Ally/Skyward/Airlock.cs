using System.Collections.Generic;
using UnityEngine;

public class Airlock : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Airlock";
        AnimName = "testEff";
        ReachablePositions = new List<int> { 3, 4, 5 };
        UsableFromPositions = new List<int> {0,1, 2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 4, 7, 8 } };
        skillValuesMax = new List<List<int>> { new List<int> { 6, 10, 12 } };
        effectLenghts = new List<List<int>> { new List<int> { 1, 1, 2 } };
        Level = 0;
        IconId = 4;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers)
        {
            Effect stn = new StunEffect(effectLenghts[0][Level]);
            targetMember.Damage(skillValues[0], false);
            stn.Attach(targetMember);
        }
    }
}
