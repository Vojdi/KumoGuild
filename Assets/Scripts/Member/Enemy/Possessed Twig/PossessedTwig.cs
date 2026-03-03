using UnityEngine;
using System.Collections.Generic;
public class PossessedTwig : EnemyMember
{
    protected override void Awake()
    {
        base.Awake();
        Skills.Add(Skill.Create<Splintersnipe>(this));
        Skills.Add(Skill.Create<SplinterRain>(this));
    }
}
