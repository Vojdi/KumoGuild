using System.Collections.Generic;

public class PiercingLight : Skill
{
    void OnEnable()
    {
        skillName = "Piercing Light";
        AnimName = "testEff";
        reachablePositions = new List<int> { 3,4 };
        usableFromPositions = new List<int> { 2};
        SkillType = "area";
        skillValue = 4;
        SelfOnly = false;
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
