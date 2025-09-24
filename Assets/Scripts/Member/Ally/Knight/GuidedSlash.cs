using System.Collections.Generic;

public class GuidedSlash : Skill
{
    void OnEnable()
    {
        skillName = "Guided Slash";
        reachablePositions = new List<int> { 4, 5 };
        usableFromPositions = new List<int> { 1, 2 };
        SkillType = "single";
        skillValue = 5;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        targetMember.Damage(skillValue);
    }
}
