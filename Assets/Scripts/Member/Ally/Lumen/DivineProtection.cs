using System.Collections.Generic;
using UnityEngine;
using System;

public class DivineProtection : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Divine Protection";
        AnimName = "divineProtection";
        SkillRangeType = "multi";
        skillType = "support";

        ReachablePositions = new List<int> { 0, 1, 2 };
        UsableFromPositions = new List<int> { 0, 1 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectTypes = new List<Type> {typeof(ProtEffect)};
        effectLengths = new List<List<int>> { new List<int> {2,2,3 }};
        effectValues = new List<List<int>> { new List<int> { 35, 50, 65 } };
        effectValuesSelf = new List<bool> {false};

        Level = 0;
        IconId = 8;
    }
}
