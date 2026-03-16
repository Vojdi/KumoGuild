using System.Collections.Generic;
using UnityEngine;
using System;

public class SunMending : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Sun Mending";
        AnimName = "sunMending";
        SkillRangeType = "single";
        skillType = "support";

        ReachablePositions = new List<int> { 0, 1};
        UsableFromPositions = new List<int> { 0, 1};

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { -16, -13, } };
        skillValuesMax = new List<List<int>> { new List<int> { -21, -18, } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 7 ;
    }
}
