using UnityEngine;

public class Lumen : AllyMember
{
    protected override void Awake()
    {
        maxHealth = 25;
        speed = 7;
        IconId = 5;
        base.Awake();
        Skills.Add(Skill.Create<LightningSpear>(this));
        Skills.Add(Skill.Create<SunMending>(this));
        Skills.Add(Skill.Create<DivineProtection>(this));
        Skills.Add(Skill.Create<RayOfSunlight>(this));
    }
}
