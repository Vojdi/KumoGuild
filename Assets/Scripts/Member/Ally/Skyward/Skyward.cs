using UnityEngine;

public class Skyward : AllyMember
{
    protected override void Awake()
    {
        memberName = "Skyward";
        maxHealth = 35;
        speed = 3;
        base.Awake();
        Skills.Add(Skill.Create<AirSlice>(this));
        Skills.Add(Skill.Create<WindCall>(this));
        Skills.Add(Skill.Create<SkyJavelin>(this));
        Skills.Add(Skill.Create<Airlock>(this));
        Skills.Add(Skill.Create<UnitySkill>(this));

    }
}
