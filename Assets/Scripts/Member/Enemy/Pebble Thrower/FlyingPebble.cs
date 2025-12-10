using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FlyingPebble : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Flying Pebble";
        AnimName = "testEff";
        SkillRangeType = "single";

        ReachablePositions = new List<int> {0,1,2 };
       
        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 10, 4, 5 } };
        skillValuesMax = new List<List<int>> { new List<int> { 12, 6, 7 } };
        skillValuesSelf = new List<bool> { false };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
    }
}
