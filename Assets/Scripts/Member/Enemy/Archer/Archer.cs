using UnityEngine;

public class Archer : EnemyMember
{
    protected override void Awake()
    {
        base.Awake();
        Skills.Add(Skill.Create<Waterbolt>(this));
        Skills.Add(Skill.Create<BleedOut>(this));
    }
}
