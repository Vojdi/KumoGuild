using UnityEngine;

public class MightyBow : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 3;
        speed = 6;
        base.Awake();
        Skills.Add(Skill.Create<WaterchargedShot>(this));
    }
}
