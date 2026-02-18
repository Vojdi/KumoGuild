using UnityEngine;

public class Archer : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 3;
        speed = 2;
        base.Awake();
        Skills.Add(Skill.Create<Waterbolt>(this));
        Skills.Add(Skill.Create<BleedOut>(this));
    }
}
