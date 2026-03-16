using System.Collections.Generic;
using UnityEngine;
using System;

public class Stab : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Stab Wound";
        AnimName = "stab";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3, 4, };
        UsableFromPositions = new List<int> { 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 9, 12} };
        skillValuesMax = new List<List<int>> { new List<int> { 12, 15 } };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 3, 3} };
        effectValues = new List<List<int>> { new List<int> { 3, 4} };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
        IconId = 17;
    }
}
