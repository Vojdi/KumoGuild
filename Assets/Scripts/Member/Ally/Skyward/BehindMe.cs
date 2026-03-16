using System.Collections.Generic;
using System;
public class BehindMe : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Behind Me!";
        AnimName = "behindme";
        SkillRangeType = "single";
        skillType = "support";

        ReachablePositions = new List<int>();
        UsableFromPositions = new List<int>() { 2 };

        HasSelfSkill = true;
        SelfOnly = true;

        skillValuesMin = new List<List<int>>();
        skillValuesMax = new List<List<int>>();
        skillValuesSelf = new List<bool>();

        effectLengths = new List<List<int>> { new List<int> { 2, 3},new List<int>{2,2 }};
        effectValues = new List<List<int>> { new List<int> {25, 45}, new List<int> { 0, 0}};
        effectTypes = new List<Type> { typeof(ProtEffect), typeof(TauntEffect)};
        effectValuesSelf = new List<bool> {true,true};

        Level = 0;
        IconId = 3;
    }
}
