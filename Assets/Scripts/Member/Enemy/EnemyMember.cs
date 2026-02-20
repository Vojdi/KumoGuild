using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyMember : Member
{
    [SerializeField] List<int> desiredPositions;
    public List<int> DesiredPositions => desiredPositions;
    public override void YourTurn()
    {
        base.YourTurn();
        if (!stunnedThisRound)
        {
            Debug.Log($"It is {this}'s turn");

            int tauntingMemberPosition = -1;
            foreach (Member m in GameManager.Instance.Members)
            {
                if (m.Effects.OfType<TauntEffect>().Any() && m is AllyMember)
                {
                    tauntingMemberPosition = m.Position;
                }
            }

            List<Skill> availableSkills = new List<Skill>();
            string debug = "";
            foreach (var skill in Skills)
            {
                if (SkillWorthUsingCheck(skill, tauntingMemberPosition))
                {
                    availableSkills.Add(skill);
                    debug += $", {skill.ToString()}";
                }
            }
            Debug.Log($"available skill to use {debug}");

            if (availableSkills.Count == 0)//////
            {
                Debug.Log("no skill found");///////
                return;//////
            }/////

            Skill SelectedSkill = availableSkills[Random.Range(0, availableSkills.Count)];
            List<int> validTargetPositions = new List<int>();
            int position;
            if (tauntingMemberPosition == -1)
            {
                foreach (var member in GameManager.Instance.Members)
                {
                    if (SelectedSkill.ReachablePositions.Contains(member.Position))
                    {
                        validTargetPositions.Add(member.Position);
                    }
                }
                if (SelectedSkill.SelfOnly)
                {
                    validTargetPositions.Add(this.Position);
                }
                position = validTargetPositions[Random.Range(0, validTargetPositions.Count)];
            }
            else
            {
                position = tauntingMemberPosition;
            }

            Debug.Log($"{this} used {SelectedSkill.SkillName}");
            VisualEffectManager.Instance.PlayEffectAnimation(SelectedSkill, position);
        }    
    }
    bool SkillWorthUsingCheck(Skill skill, int tauntingMemberPosition) {

        if (skill.SelfOnly)
        {
            return true;
        }
        if (tauntingMemberPosition != -1)
        {
            if (!skill.ReachablePositions.Contains(tauntingMemberPosition))
            {
                return false;
            }
        }
        if (skill.SkillRangeType == "single")
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (skill.ReachablePositions.Contains(m.Position))
                {
                    return true;
                }
            }
            return false;
        }
        else if (skill.SkillRangeType == "multi")
        {
            List<int> memberPositions = new List<int>();
            foreach (Member m in GameManager.Instance.Members)
            {
                memberPositions.Add(m.Position);
            }

            foreach (int pos in skill.ReachablePositions)
            {
                if (!memberPositions.Contains(pos))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
