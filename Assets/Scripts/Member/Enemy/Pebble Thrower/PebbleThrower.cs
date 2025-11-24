using UnityEngine;
public class PebbleThrower : EnemyMember
{
    protected override void Awake()
    {
        memberName = "Basic Enemy";
        maxHealth = 50;
        speed = 1;
        base.Awake();
        Skills.Add(Skill.Create<FlyingPebble>(this));
        Skills.Add(Skill.Create<Scattershot>(this));
    }
}
