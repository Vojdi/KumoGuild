using System.Collections.Generic;
using UnityEngine;
using System;

public class YarnBall : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Yarn Ball";
        AnimName = "yarnBall";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3, 4, 5 };
        UsableFromPositions = new List<int> { 0, 1 };

        SelfOnly = false;
        HasSelfSkill = false;

        skillValuesMin = new List<List<int>> { new List<int> { 2,5} };
        skillValuesMax = new List<List<int>> { new List<int> { 4,7} };
        skillValuesSelf = new List<bool> { false };

        effectLengths = new List<List<int>> { new List<int> { 1,2} };
        effectValues = new List<List<int>> { new List<int> { 0,0} };
        effectTypes = new List<Type> { typeof(StunEffect) };
        effectValuesSelf = new List<bool> { false };

        Level = 0;
        IconId = 18;
    }
}
