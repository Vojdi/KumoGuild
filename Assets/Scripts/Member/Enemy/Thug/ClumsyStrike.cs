using System.Collections.Generic;

public class ClumsyStrike : Skill
{
    void OnEnable()
    {
        skillName = "Clumsy Strike";
        AnimName = "testEff";
        reachablePositions = new List<int> {0, 1, 2 };
        SkillType = "single";
        SelfOnly = false;
        skillValuesMin = new List<int> { 1, 2, 3 };
        skillValuesMax = new List<int> { 3, 5, 6 };
        effectLenghts = new List<int> { 1, 2, 3 };
        effectValues = new List<int> { 0, 0, 0 };
        level = 0;
    }
    public override void UseSkill(int targetPosition)
    {
        base.UseSkill(targetPosition);
        foreach (Member targetMember in targetMembers)
        {
            StunEffect stunEff = new StunEffect();
            stunEff.SetValues(targetMember, effectLenghts[level]);
            stunEff.Attach();
            targetMember.Damage(skillValue,false);
        }
    }
}
