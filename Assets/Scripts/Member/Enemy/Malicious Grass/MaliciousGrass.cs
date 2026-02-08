using UnityEngine;
using System.Collections.Generic;

public class MaliciousGrass : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 3;
        speed = 6;
        base.Awake();
        Skills.Add(Skill.Create<GrassPoke>(this));
        //Skills.Add(Skill.Create<GrassPrison>(this));
    }
}
