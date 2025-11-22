using System.Collections.Generic;
using UnityEngine;

public class UnitySkill : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Unity";
        AnimName = "testEff";
        ReachablePositions = new List<int> { 0,1,2 };
        UsableFromPositions = new List<int> { 0,1,2 };
        SkillType = "area";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 0, 0, 0 } };
        skillValuesMax = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4 }};
        effectValues = new List<List<int>> { new List<int> { 20, 30, 40 }};
        Level = 0;
        IconId = 5;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (var targetMember in targetMembers)
        {
            Effect protEff = new ProtEffect(effectLenghts[0][Level], effectValues[0][Level]);
            protEff.Attach(targetMember);
        }
    }
}
