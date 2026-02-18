using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterchargedShot : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Watercharged Arrow";
        AnimName = "watershot";
        SkillRangeType = "single";

        ReachablePositions = new List<int> { 0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 10, 12, 14 } };
        skillValuesMax = new List<List<int>> { new List<int> { 20, 24, 28 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
