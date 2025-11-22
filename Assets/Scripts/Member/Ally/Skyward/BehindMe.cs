using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BehindMe : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Behind me";
        AnimName = "testEff";
        SkillType = "single";
        ReachablePositions = new List<int>();
        UsableFromPositions = new List<int>() {2};
        skillValuesMin = new List<List<int>> { new List<int> { 0, 0, 0 } };
        skillValuesMax = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4},new List<int>{2,2,2 }};
        effectValues = new List<List<int>> { new List<int> {20, 30, 40}, new List<int> { 0, 0, 0 } };
        Level = 0;
        HasSelfSkill = true;
        SelfOnly = true;
        IconId = 3;
    }
    public override void SelfUseSkill()
    {
        Effect tntEff = new TauntEffect(effectLenghts[1][Level]);
        tntEff.Attach(SelfMember);
        ProtEffect protEff = new ProtEffect(effectLenghts[0][Level], effectValues[0][Level]);
        protEff.Attach(SelfMember);
    }
}
