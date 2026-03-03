using UnityEngine;

public class MightyBow : EnemyMember
{
    protected override void Awake()
    {
        base.Awake();
        Skills.Add(Skill.Create<WaterchargedShot>(this));
    }
}
