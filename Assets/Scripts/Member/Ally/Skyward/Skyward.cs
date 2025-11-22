using UnityEngine;

public class Skyward : AllyMember
{
    protected override void Awake()
    {
        memberName = "Skyward";
        maxHealth = 35;
        speed = 3;
        IconId = 0;
        base.Awake();
        Skills.Add(Skill.Create<AirSlice>(this));
        Skills.Add(Skill.Create<SkyLance>(this));
        Skills.Add(Skill.Create<BehindMe>(this));
        Skills.Add(Skill.Create<Airlock>(this));
        Skills.Add(Skill.Create<UnitySkill>(this));
    }
}
