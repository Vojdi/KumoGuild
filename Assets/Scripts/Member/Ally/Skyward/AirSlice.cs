using System.Collections.Generic;
using UnityEngine;

public class AirSlice : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Air Slice";
        AnimName = "slash";
        ReachablePositions = new List<int> { 3, 4,};
        UsableFromPositions = new List<int> {1, 2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 8, 11, 14 } };
        skillValuesMax = new List<List<int>> { new List<int> { 15, 17, 20 } };
        effectLenghts = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectValues = new List<List<int>> { new List<int> { 0, 0, 0 } };
        Level = 0;
        IconId = 1;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers) {
            targetMember.Damage(skillValues[0], false);
        }
    }
}
