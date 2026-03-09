using UnityEngine;

public class DepressedRock : EnemyMember
{
    protected override void Awake()
    {
        base.Awake();
        Skills.Add(Skill.Create<BoulderThrow>(this));
        Skills.Add(Skill.Create<MountainRise>(this));
    }
}
