using System.Collections.Generic;

public class SkyLance : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Sky Lance";
        AnimName = "testEff";
        ReachablePositions = new List<int> { 3,4 };
        UsableFromPositions = new List<int> { 0,1,2};
        SkillType = "area";
        SelfOnly = false;
        skillValuesMin = new List<List<int>> { new List<int> {6, 9, 11} };
        skillValuesMax = new List<List<int>> { new List<int> {11, 13, 16} };
        Level = 0;
        IconId = 2;
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
