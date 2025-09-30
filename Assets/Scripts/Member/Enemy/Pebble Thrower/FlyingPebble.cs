using System.Collections.Generic;

public class FlyingPebble : Skill
{
    void OnEnable()
    {
        skillName = "Flying Pebble";
        AnimName = "testEff";
        reachablePositions = new List<int> { 1, 2, 3 };
        SkillType = "single";
        skillValue = 5;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        targetMember.Damage(skillValue);
    }
}
