using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Insult : Skill
{
    void OnEnable()
    {
        SkillName = "Insult";
        AnimName = "testEff";
        reachablePositions = new List<int> { 0, 1, 2 };
        usableFromPositions = new List<int> { 0, 1, 2 };
        SkillType = "single";
        SelfOnly = true;
        skillValuesMin = new List<List<int>> { new List<int> { 0, 0, 0 } };
        skillValuesMax = new List<List<int>> { new List<int> { 0, 0, 0 } };
        effectLenghts = new List<List<int>> { new List<int> { 2, 3, 4} };
        effectValues = new List<List<int>> { new List<int> {20, 30, 40} };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers)
        {
            Effect stunEff = new TauntEffect(3);
            stunEff.Attach(targetMember);
            ProtEffect protEff = new ProtEffect(3, effectValues[0][level]);
            protEff.Attach(targetMember);
        }  
    }
}
