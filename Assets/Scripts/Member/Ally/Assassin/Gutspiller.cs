using System.Collections.Generic;
using UnityEngine;
using System;

public class Gutspiller : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Gutspiller";
        AnimName = "gutSpiller";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3 };
        UsableFromPositions = new List<int> {  1,2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 5, 8} };
        skillValuesMax = new List<List<int>> { new List<int> { 10, 13} };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 2, 2} };
        effectValues = new List<List<int>> { new List<int> { 7, 9} };
        effectTypes = new List<Type> { typeof(DoTEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
        IconId = 12;
    }
}
