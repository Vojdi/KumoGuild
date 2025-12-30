using System.Collections.Generic;
using UnityEngine;
using System;

public class RayOfSunlight : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Ray of Sunlight";
        AnimName = "rayOfSunlight";
        SkillRangeType = "multi";
        skillType = "support";

        ReachablePositions = new List<int> { 0, 1, 2 };
        UsableFromPositions = new List<int> { 0, 1 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { -4, -6, -8 } };
        skillValuesMax = new List<List<int>> { new List<int> { -6, -8, -10 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 9;
    }
}
