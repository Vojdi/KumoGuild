using UnityEngine;

public class MaliciousGrass : EnemyMember
{
    protected override void Awake()
    {
        memberName = "Malicious Grass";
        maxHealth = 35;
        speed = 6;
        base.Awake();
        Skills.Add(Skill.Create<GrassPoke>(this));
        Skills.Add(Skill.Create<GrassPrison>(this));
    }
}
