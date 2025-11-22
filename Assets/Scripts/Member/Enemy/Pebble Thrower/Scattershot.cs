using System.Collections.Generic;
public class Scattershot : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Scattershot";
        AnimName = "testEff";
        ReachablePositions = new List<int> {0, 1 };
        SkillType = "area";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> { 8, 2, 3 } };
        skillValuesMax = new List<List<int>> { new List<int> { 12, 5, 6 } };
        Level = 0;
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
