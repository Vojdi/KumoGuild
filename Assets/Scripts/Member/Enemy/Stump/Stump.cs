using UnityEngine;

public class Stump : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 3;
        speed = 10;
        Skills.Add(Skill.Create<Reinforce>(this));
    }
}
