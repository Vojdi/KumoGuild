using System.Collections.Generic;
using Unity.VisualScripting;

public class PiercingLight : Skill
{
    void OnEnable()
    {
        skillName = "Piercing Light";
        reachablePositions = new List<int> { 4, 5 };
        usableFromPositions = new List<int> { 1};
        SkillType = "area";
        skillValue = 4;
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
