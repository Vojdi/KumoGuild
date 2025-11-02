using UnityEngine;
public class PebbleThrower : EnemyMember
{
    protected override void Awake()
    {
        memberName = "Basic Enemy";
        maxHealth = 8;
        speed = 1;
        base.Awake();
        Skill.Create<FlyingPebble>(this);
        Skill.Create<Scattershot>(this);
    }
}
