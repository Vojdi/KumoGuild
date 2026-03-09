using System;
using System.Collections.Generic;
using UnityEngine;

public class MountainRise : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "mountain rise";
        AnimName = "mountain";
        SkillRangeType = "multi";

        ReachablePositions = new List<int> { 3, 4, 5 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectTypes = new List<Type> { typeof(ProtEffect) };
        effectLengths = new List<List<int>> { new List<int> { 2, 2, 3 } };
        effectValues = new List<List<int>> { new List<int> { 35, 50, 65 } };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
    }
}
