using UnityEngine;

public class MightyBow : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 30;
        speed = 6;
        base.Awake();
        Skills.Add(Skill.Create<WaterchargedShot>(this));
    }
}
