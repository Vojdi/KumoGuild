using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemyMember : Member
{
    public override void YourTurn()
    {
        
        if (health > 0)
        {
            base.YourTurn();
            if (!stunnedThisRound)
            {
                Debug.Log($"It is {gameObject.name}'s turn");////
                List<Skill> availableSkills = new List<Skill>();
                foreach (var skill in Skills)
                {
                    if (skill.SkillWorthUsingCheck())
                    {
                        availableSkills.Add(skill);
                        Debug.Log($"available skill to use {skill}");
                    }
                }
                if (availableSkills.Count == 0)//////
                {
                    Debug.Log("no skill found");///////
                    return;//////
                }/////
                Skill SelectedSkill = availableSkills[Random.Range(0, availableSkills.Count)];
                List<int> validTargetPositions = new List<int>();
                foreach (var member in GameManager.Instance.Members)
                {
                    if (SelectedSkill.ReachablePositions.Contains(member.Position))
                    {
                        validTargetPositions.Add(member.Position);
                    }
                }
                int position = validTargetPositions[Random.Range(0, validTargetPositions.Count)];
                Debug.Log($"{gameObject.name} used {SelectedSkill.SkillName}");
                VisualEffectManager.Instance.PlayEffectAnimation(SelectedSkill,position);
            }
        }
    }
}
