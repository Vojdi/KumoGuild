using System.Collections.Generic;

public class FlyingPebble : Skill
{
    void OnEnable()
    {
        skillName = "Flying Pebble";
        AnimName = "testEff";
        reachablePositions = new List<int> {0,1,2 };
        SkillType = "single";
        skillValue = 5;
        SelfOnly = false;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        targetMember.Damage(skillValue);
    }
}
