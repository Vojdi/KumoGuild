using UnityEngine;
using System.Collections.Generic;
public class PossessedTwig : EnemyMember
{
    protected override void Awake()
    {
        memberName = "Possessed Twig";
        maxHealth = 25;
        speed = 3;
        base.Awake();
        Skills.Add(Skill.Create<Splintersnipe>(this));
        Skills.Add(Skill.Create<SplinterRain>(this));
    }
}
