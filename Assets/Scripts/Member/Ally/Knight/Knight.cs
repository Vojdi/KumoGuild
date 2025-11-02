using UnityEngine;

public class Knight : AllyMember
{
    protected override void Awake()
    {
        memberName = "Cloud1";
        maxHealth = 15;
        speed = 4;
        base.Awake();
        Skill.Create<GuidedSlash>(this);
        Skill.Create<Insult>(this);
       // Skill.Create<PiercingLight>(this);
       
    }
}
