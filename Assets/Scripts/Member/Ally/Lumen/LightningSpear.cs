using System.Collections.Generic;
using UnityEngine;
using System;

public class LightningSpear : Skill
{
    override protected void OnEnable()
    {
        base.OnEnable();
        SkillName = "Lightning Spear";
        AnimName = "lightningSpear";
        SkillRangeType = "single";
        skillType = "attack";

        ReachablePositions = new List<int> { 3, 4, 5 };
        UsableFromPositions = new List<int> { 0, 1 };

        SelfOnly = false;
        HasSelfSkill = true;

        skillValuesMin = new List<List<int>> { new List<int> { 8, 11, 14 }, new List<int> { -8, -11, -14 } };
        skillValuesMax = new List<List<int>> { new List<int> { 15, 17, 20 }, new List<int> { -15, -17, -20 } };
        skillValuesSelf = new List<bool> { false, true };

        effectTypes = new List<Type>();
        effectValuesSelf = new List<bool>();

        Level = 0;
        IconId = 6;
    }
}
