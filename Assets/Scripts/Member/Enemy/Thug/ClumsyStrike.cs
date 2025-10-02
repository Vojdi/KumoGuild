using System.Collections.Generic;

public class ClumsyStrike : Skill
{
    void OnEnable()
    {
        skillName = "Clumsy Strike";
        AnimName = "testEff";
        reachablePositions = new List<int> {0, 1, 2 };
        SkillType = "single";
        skillValue = 4;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        targetMember.Damage(skillValue);
    }
}
