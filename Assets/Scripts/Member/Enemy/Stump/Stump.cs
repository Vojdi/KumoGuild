using UnityEngine;

public class Stump : EnemyMember
{
    protected override void Awake()
    {
        base.Awake();
        Skills.Add(Skill.Create<Reinforce>(this));
    }
}
