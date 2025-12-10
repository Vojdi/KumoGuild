using System;
using System.Collections.Generic;

public class SkyLance : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Sky Lance";
        AnimName = "testEff";
        SkillRangeType = "multi";
        skillType = "attack";

        ReachablePositions = new List<int> { 3,4 };
        UsableFromPositions = new List<int> { 0,1,2};
        
        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> {6, 9, 11} };
        skillValuesMax = new List<List<int>> { new List<int> {11, 13, 16} };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 2;
    }
}
