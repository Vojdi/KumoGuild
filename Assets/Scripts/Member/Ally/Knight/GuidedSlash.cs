using System.Collections.Generic;
using UnityEngine;

public class GuidedSlash : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Guided Slash";
        AnimName = "slash";
        ReachablePositions = new List<int> { 3, 4, 5 };
        UsableFromPositions = new List<int> {0, 1, 2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 3, 4, 5 } };
        skillValuesMax = new List<List<int>> { new List<int> { 6, 7, 8 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4 } };
        effectValues = new List<List<int>> { new List<int> { 1, 2, 3 } };
        Level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers) {
            DoTEffect dotEff = new DoTEffect(effectLenghts[0][Level], effectValues[0][Level]);
            dotEff.Attach(targetMember);
            targetMember.Damage(skillValues[0], false);
        }
    }
}
