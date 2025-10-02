using System.Collections.Generic;
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
        skillValue = 3;
    }
    public override void UseSkill(int targetPosition)
    {
        Member targetMember = SingleTarget(targetPosition);
        Effect stunEff = new TauntEffect();
        stunEff.SetValues(targetMember, skillValue);
        stunEff.Attach(targetMember);
    }
}
