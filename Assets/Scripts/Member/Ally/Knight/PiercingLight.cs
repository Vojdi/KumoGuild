using System.Collections.Generic;

public class PiercingLight : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Piercing Light";
        AnimName = "testEff";
        reachablePositions = new List<int> { 3,4 };
        usableFromPositions = new List<int> { 2};
        SkillType = "area";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> {2, 3, 4} };
        skillValuesMax = new List<List<int>> { new List<int> {4, 5, 6} };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (var targetMember in targetMembers)
        {
            targetMember.Damage(skillValues[0], false);
        }
    }
}
