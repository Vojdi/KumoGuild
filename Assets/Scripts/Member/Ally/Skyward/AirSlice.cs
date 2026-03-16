using System.Collections.Generic;
using UnityEngine;
using System;

public class AirSlice : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Air Slice";
        AnimName = "airslice";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3, 4,};
        UsableFromPositions = new List<int> {1, 2 };
       
        SelfOnly = false;
        HasSelfSkill = false;
        
        skillValuesMin = new List<List<int>> { new List<int> {13, 16} };
        skillValuesMax = new List<List<int>> { new List<int> { 18, 21 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 1;
    }
}
