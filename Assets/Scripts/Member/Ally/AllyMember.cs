using System.Collections.Generic;
using UnityEngine;

public class AllyMember : Member
{
    public override void YourTurn()
    {
        if (health > 0)
        {
            if (health > 0)
            {
                Debug.Log($"It is {gameObject.name}'s turn");////
                List<Skill> availableSkills = new List<Skill>();
                foreach (var skill in skills)
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
                Skill chosenSKill = availableSkills[Random.Range(0, availableSkills.Count)];
                List<int> validTargetPositions = new List<int>();
                foreach (var member in GameManager.Instance.Members)
                {
                    if (chosenSKill.ReachablePositions.Contains(member.Position))
                    {
                        validTargetPositions.Add(member.Position);
                    }
                }
                int position = validTargetPositions[Random.Range(0, validTargetPositions.Count)];
                Debug.Log($"{gameObject.name} used {chosenSKill.SkillName} on member on position {position}");//////
                chosenSKill.UseSkill(position);
                GameManager.Instance.NextTurn();
            }
        }
    }
}
