using System.Collections.Generic;
using UnityEngine;
using System;

public class GrassPoke : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Grass Poke";
        AnimName = "grassPoke";
        SkillRangeType = "single";

        ReachablePositions = new List<int> { 0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 7, 9, 11 } };
        skillValuesMax = new List<List<int>> { new List<int> { 12, 14, 16 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
