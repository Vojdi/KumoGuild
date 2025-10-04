using System.Collections.Generic;
public class Scattershot : Skill
{
    void OnEnable()
    {
        skillName = "Scattershot";
        AnimName = "testEff";
        reachablePositions = new List<int> {0, 1 };
        SkillType = "area";
        SelfOnly = false;
        skillValuesMin = new List<int> { 1, 2, 3 };
        skillValuesMax = new List<int> { 3, 5, 6 };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);  
        List<Member> targetMembers = AreaAttack(targetPosition);
        foreach (var targetMember in targetMembers)
        {
            targetMember.Damage(skillValue);
        }
    }
}
