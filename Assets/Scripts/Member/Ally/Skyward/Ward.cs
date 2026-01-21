using UnityEngine;

public class Ward : AllyMember
{
    protected override void Awake()
    {
        memberName = "Ward";
        maxHealth = 40;
        speed = 3;
        IconId = 0;
        base.Awake();
        Skills.Add(Skill.Create<AirSlice>(this));
        Skills.Add(Skill.Create<SkyLance>(this));
        Skills.Add(Skill.Create<BehindMe>(this));
        Skills.Add(Skill.Create<Airlock>(this));
    }
}
