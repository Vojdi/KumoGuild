using UnityEngine;

public class Knight : AllyMember
{
    protected override void Awake()
    {
        memberName = "Cloud1";
        maxHealth = 15;
        speed = 4;
        base.Awake();
        Skills.Add(Skill.Create<GuidedSlash>(this));
        Skills.Add(Skill.Create<Insult>(this));
       // Skills.Add(Skill.Create<PiercingLight>(this));
       
    }
}
