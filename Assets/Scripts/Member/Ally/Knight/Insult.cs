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
        effectLenghts = new List<int> { 2, 3, 4 };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        
        Member targetMember = SingleTarget(targetPosition);
        Effect stunEff = new TauntEffect();
        stunEff.SetValues(targetMember, 3);
        stunEff.Attach();
    }
}
