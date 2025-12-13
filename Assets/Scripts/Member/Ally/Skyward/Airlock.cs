using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Airlock : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Airlock";
        AnimName = "airlock";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3, 4, 5 };
        UsableFromPositions = new List<int> {0,1, 2 };
       
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
        IconId = 4;
    }
}
