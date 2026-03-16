using System.Collections.Generic;
using UnityEngine;
using System;

public class KnifeThrow : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Knife Throw";
        AnimName = "knifeThrow";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> {4,5};
        UsableFromPositions = new List<int> {0, 1  };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 4,7 } };
        skillValuesMax = new List<List<int>> { new List<int> { 8, 11} };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 3, 3} };
        effectValues = new List<List<int>> { new List<int> { 3,4 } };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
        IconId = 13;
    }
}
