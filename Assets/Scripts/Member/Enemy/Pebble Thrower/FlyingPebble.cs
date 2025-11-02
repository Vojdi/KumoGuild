using System.Collections.Generic;

public class FlyingPebble : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Flying Pebble";
        AnimName = "testEff";
        ReachablePositions = new List<int> {0,1,2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 3, 4, 5 } };
        skillValuesMax = new List<List<int>> { new List<int> { 5, 6, 7 } };
        Level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers) {
            targetMember.Damage(skillValues[0], false);
        }
    }
}
