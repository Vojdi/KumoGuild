using System;
using System.Collections.Generic;
public class Scattershot : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Scattershot";
        AnimName = "testEff";
        SkillRangeType = "multi";

        ReachablePositions = new List<int> {0, 1 };
       
        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 8, 2, 3 } };
        skillValuesMax = new List<List<int>> { new List<int> { 12, 5, 6 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
