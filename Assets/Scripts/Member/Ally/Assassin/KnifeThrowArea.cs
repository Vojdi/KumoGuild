using System.Collections.Generic;
using UnityEngine;
using System;

public class KnifeThrowArea : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Knife Throw Area";
        AnimName = "KnifeThrowArea";
        SkillRangeType = "area";
        skillType = "attack";

        ReachablePositions = new List<int> {  3,4,5 };
        UsableFromPositions = new List<int> { 0, 1 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 8, 11, 14 } };
        skillValuesMax = new List<List<int>> { new List<int> { 15, 17, 20 } };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 3, 3, 3 } };
        effectValues = new List<List<int>> { new List<int> { 3, 4, 5 } };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
        IconId = 14;
    }
}
