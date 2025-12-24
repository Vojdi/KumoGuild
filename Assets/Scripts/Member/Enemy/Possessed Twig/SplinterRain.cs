using System.Collections.Generic;
using System;
using UnityEngine;

public class SplinterRain : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Splinter Rain";
        AnimName = "splinterRain";
        SkillRangeType = "multi";

        ReachablePositions = new List<int> { 0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 3, 5, 7 } };
        skillValuesMax = new List<List<int>> { new List<int> { 5, 7, 9 } };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 3, 3, 3 } };
        effectValues = new List<List<int>> { new List<int> { 3, 4, 5 } };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
    }
}
