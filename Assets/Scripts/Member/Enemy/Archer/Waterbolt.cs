using System.Collections.Generic;
using UnityEngine;
using System;

public class Waterbolt : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Waterbolt";
        AnimName = "waterbolt";
        SkillRangeType = "multi";

        ReachablePositions = new List<int> {0,1,2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 7  } };
        skillValuesMax = new List<List<int>> { new List<int> { 12 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
