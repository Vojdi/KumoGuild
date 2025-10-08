using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Insult : Skill
{
    void OnEnable()
    {
        skillName = "Insult";
        AnimName = "testEff";
        reachablePositions = new List<int> { 0,1,2 };
        usableFromPositions = new List<int> { 0, 1, 2 };
        SkillType = "single";
        SelfOnly = true;
        skillValuesMin = new List<int> { 0, 0, 0 };
        skillValuesMax = new List<int> { 0, 0, 0 };
        effectLenghts = new List<int> { 2, 3, 4 };
        effectValues = new List<int> { 20, 30, 40 };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers)
        {
            Effect stunEff = new TauntEffect();
            stunEff.SetValues(targetMember, 3);
            stunEff.Attach();
            ProtEffect protEff = new ProtEffect();
            protEff.SetValues(targetMember, 3, effectValues[0]);
            protEff.Attach();
        }  
    }
}
