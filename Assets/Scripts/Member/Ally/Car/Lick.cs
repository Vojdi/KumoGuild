using System.Collections.Generic;
using UnityEngine;
using System;
public class Lick : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Lick";
        AnimName = "lick";
        SkillRangeType = "single";
        skillType = "support";

        ReachablePositions = new List<int> { 0, 1, 2 };
        UsableFromPositions = new List<int> { 0, 1, 2};

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { -14, -11 } };
        skillValuesMax = new List<List<int>> { new List<int> { -22, -20} };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 15;
    }
}
