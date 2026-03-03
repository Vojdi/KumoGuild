using UnityEngine;

public class Assassin : AllyMember
{
    protected override void Awake()
    {
        IconId = 20;
        base.Awake();
        Skills.Add(Skill.Create<Stab>(this));
        Skills.Add(Skill.Create<KnifeThrow>(this));
        Skills.Add(Skill.Create<KnifeThrowArea>(this));
        Skills.Add(Skill.Create<Gutspiller>(this));
    }
}
