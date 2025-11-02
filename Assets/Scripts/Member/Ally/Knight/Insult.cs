using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Insult : Skill
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SkillName = "Insult";
        AnimName = "testEff";
        SkillType = "single";
        ReachablePositions = new List<int>();
        UsableFromPositions = new List<int>();
        skillValuesMin = new List<List<int>> { new List<int> { 0, 0, 0 } };
        skillValuesMax = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4} };
        effectValues = new List<List<int>> { new List<int> {20, 30, 40} };
        Level = 0;
        HasSelfSkill = true;
        SelfOnly = true;
    }
    public override void SelfUseSkill()
    {
        Effect stunEff = new TauntEffect(3);
        stunEff.Attach(selfMember);
        ProtEffect protEff = new ProtEffect(3, effectValues[0][Level]);
        protEff.Attach(selfMember);
    }
}
