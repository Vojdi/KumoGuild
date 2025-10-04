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
        SelfOnly = false;
        skillValuesMin = new List<int> { 2, 3, 4 };
        skillValuesMax = new List<int> { 4,5, 6 };
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
