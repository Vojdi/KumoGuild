using System.Collections.Generic;

public class GuidedSlash : Skill
{
    void OnEnable()
    {
        skillName = "Guided Slash";
        AnimName = "testEff";
        reachablePositions = new List<int> { 3, 4, 5 };
        usableFromPositions = new List<int> {0, 1, 2 };
        SkillType = "single";
        skillValue = 5;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        Effect stunEff = new StunEffect();
        stunEff.SetValues(targetMember, 1);
        stunEff.Attach(targetMember);
        targetMember.Damage(skillValue);
    }
}
