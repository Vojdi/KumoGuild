using System;
using System.Collections.Generic;
using UnityEngine;

public class BoulderThrow : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Boulder Throw";
        AnimName = "boulderThrow";
        SkillRangeType = "single";

        ReachablePositions = new List<int> { 0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 11} };
        skillValuesMax = new List<List<int>> { new List<int> { 14} };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
