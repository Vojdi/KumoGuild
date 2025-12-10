using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitySkill : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Unity";
        AnimName = "testEff";
        SkillRangeType = "multi";
        skillType = "support";

        ReachablePositions = new List<int> { 0,1,2 };
        UsableFromPositions = new List<int> { 0,1,2 };
        
        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectLengths = new List<List<int>> { new List<int> { 2, 3, 4 }};
        effectValues = new List<List<int>> { new List<int> { 35, 50, 65 }};
        effectTypes = new List<Type> { typeof(ProtEffect)};
        effectValuesSelf = new List<bool> {false };
        
        Level = 0;
        IconId = 5;
    }
}
