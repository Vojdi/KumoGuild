using UnityEngine;

public class Stump : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 30;
        speed = 10;
        base.Awake();
        Skills.Add(Skill.Create<Reinforce>(this));
    }
}
