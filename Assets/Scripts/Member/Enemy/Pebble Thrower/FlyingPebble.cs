using System.Collections.Generic;

public class FlyingPebble : Skill
{
    void OnEnable()
    {
        skillName = "Flying Pebble";
        AnimName = "testEff";
        reachablePositions = new List<int> {0,1,2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<int> { 3, 4, 5 };
        skillValuesMax = new List<int> { 5, 6, 7 };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);  
        Member targetMember = SingleTarget(targetPosition);
        targetMember.Damage(skillValue);
    }
}
