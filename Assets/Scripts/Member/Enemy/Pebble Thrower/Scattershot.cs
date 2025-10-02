using System.Collections.Generic;
public class Scattershot : Skill
{
    void OnEnable()
    {
        skillName = "Scattershot";
        AnimName = "testEff";
        reachablePositions = new List<int> {0, 1 };
        SkillType = "area";
        skillValue = 3;
    }
    public override void UseSkill(int targetPosition)
    {
        List<Member> targetMembers = AreaAttack(targetPosition);
        foreach (var targetMember in targetMembers)
        {
            targetMember.Damage(skillValue);
        }
    }
}
