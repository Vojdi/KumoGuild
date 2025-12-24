using System.Collections.Generic;
using UnityEngine;
using System;

public class GrassPrison : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Grass Prison";
        AnimName = "grassPrison";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 0, 1, 2 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 4, 7, 8 } };
        skillValuesMax = new List<List<int>> { new List<int> { 6, 10, 12 } };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 1, 1, 2 } };
        effectValues = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectTypes = new List<Type> { typeof(StunEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
    }
}
