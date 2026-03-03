using UnityEngine;

public class Car : AllyMember
{
    protected override void Awake()
    {
        IconId = 19;
        base.Awake();
        Skills.Add(Skill.Create<Claw>(this));
        Skills.Add(Skill.Create<YarnBall>(this));
        Skills.Add(Skill.Create<Lick>(this));
        Skills.Add(Skill.Create<Purr>(this));
    }
}
