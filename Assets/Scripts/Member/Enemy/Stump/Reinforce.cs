using System.Collections.Generic;
using UnityEngine;
using System;

public class Reinforce : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Reinforce";
        AnimName = "reinforce";
        SkillRangeType = "single";

        ReachablePositions = new List<int> {4,5 };

        HasSelfSkill = true;
        SelfOnly = true;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectLengths = new List<List<int>> { new List<int> { 2, 3, 4 }, new List<int> { 2, 2, 2 } };
        effectValues = new List<List<int>> { new List<int> { 35, 50, 65 }, new List<int> { 0, 0, 0 } };
        effectTypes = new List<Type> { typeof(ProtEffect), typeof(TauntEffect) };
        effectValuesSelf = new List<bool> { true, true };

        Level = 0;
    }
}
