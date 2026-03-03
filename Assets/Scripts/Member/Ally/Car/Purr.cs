using System;
using System.Collections.Generic;
using UnityEngine;

public class Purr : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Purring";
        AnimName = "purr";
        SkillRangeType = "single";
        skillType = "support";

        ReachablePositions = new List<int>();
        UsableFromPositions = new List<int>() { 0, 1, 2 };

        HasSelfSkill = false;
        SelfOnly = false;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectLengths = new List<List<int>> { new List<int> { 2, 3, 4 }, new List<int> { 2, 2, 2 } };
        effectValues = new List<List<int>> { new List<int> { 35, 50, 65 }, new List<int> { 0, 0, 0 } };
        effectTypes = new List<Type> { typeof(ProtEffect), typeof(TauntEffect) };
        effectValuesSelf = new List<bool> { false, false };

        Level = 0;
        IconId =16;
    }
}
