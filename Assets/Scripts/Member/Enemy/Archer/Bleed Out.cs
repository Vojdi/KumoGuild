using System.Collections.Generic;
using UnityEngine;
using System;

public class BleedOut : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Bleed Out";
        AnimName = "bleedOut";
        SkillRangeType = "single";

        ReachablePositions = new List<int> {0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 14 } };
        skillValuesMax = new List<List<int>> { new List<int> { 16 } };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 3, 3, 3 } };
        effectValues = new List<List<int>> { new List<int> { 3, 4, 5 } };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> {false };

        Level = 0;
    }
}
