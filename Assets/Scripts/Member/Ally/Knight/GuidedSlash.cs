using System.Collections.Generic;

public class GuidedSlash : Skill
{
    void OnEnable()
    {
        SkillName = "Guided Slash";
        AnimName = "slash";
        reachablePositions = new List<int> { 3, 4, 5 };
        usableFromPositions = new List<int> {0, 1, 2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 3, 4, 5 } };
        skillValuesMax = new List<List<int>> { new List<int> { 6, 7, 8 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4 } };
        effectValues = new List<List<int>> { new List<int> { 1, 2, 3 } };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers) {
            DoTEffect dotEff = new DoTEffect(effectLenghts[0][level], effectValues[0][level]);
            dotEff.Attach(targetMember);
            targetMember.Damage(skillValues[0], false);
        }
    }
}
