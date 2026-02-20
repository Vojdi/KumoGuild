using UnityEngine;

public class Archer : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 30;
        speed = 2;
        base.Awake();
        Skills.Add(Skill.Create<Waterbolt>(this));
        Skills.Add(Skill.Create<BleedOut>(this));
    }
}
