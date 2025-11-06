using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] Animator darkenOverlayAnimator;
    [SerializeField] GameObject newTurnTextGameObject;
    [SerializeField] GameObject skillUsedTextGameObject;
    static VisualEffectManager instance;
    public static VisualEffectManager Instance => instance;
    Member memberOnTurn;
    List<Member> targetMembers;
    List<Member> prevTargetables;

    Animator animator;
    Skill attackingSkill;
    int attackingPos;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        targetMembers = new List<Member>();
        prevTargetables = new List<Member>();
    }
    public void PlayEffectAnimation(Skill skill, int pos)
    {
        targetMembers.Clear();
        attackingSkill = skill;
        attackingPos = pos;
        skillUsedTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"Skill: {skill.SkillName}";
        if (memberOnTurn is EnemyMember)
        {
            skillUsedTextGameObject.GetComponent<Animator>().Play("enemyUsedSkill", 0, 0);
        }
        else
        {
            skillUsedTextGameObject.GetComponent<Animator>().Play("allyUsedSkill", 0, 0);
        }

        if (skill.SkillType == "area")
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (attackingSkill.ReachablePositions.Contains(m.Position))
                {
                    targetMembers.Add(m);
                }  
            }
        }
        else
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (m.Position == attackingPos)
                {
                    targetMembers.Add(m);
                }
            }
        }
        foreach (Member m in targetMembers)
        {
            m.TargetedIndicatorAnimator.Play("targetIndicatorAppear",0,0);
        }
    }
    public void SkillAnncounced()
    {
        memberOnTurn.TurnIndicatorAnimator.Play("turnIndicatorDissappear");
        foreach(Member m in targetMembers)
        {
            m.TargetedIndicatorAnimator.Play("targetIndicatorDissapear");
        }   
        darkenOverlayAnimator.Play("darken", 0, 0);
    }
    public void Darkened()
    {
        animator.Play(attackingSkill.AnimName, 0, 0);
    }
    public void Lightened()
    {
        if (attackingSkill.SelfOnly)
        {
            attackingSkill.SelfUseSkill();
        }else if (attackingSkill.HasSelfSkill)
        {
            attackingSkill.UseSkill(attackingPos);
            attackingSkill.SelfUseSkill();
        }else
        {
            attackingSkill.UseSkill(attackingPos);
        }
        GameManager.Instance.NextTurn();
    }
    public void EffectEnded()
    {
        darkenOverlayAnimator.Play("lighten", 0, 0);
    }
    public void NewTurnEffect(Member member)
    {
        memberOnTurn = member;
        memberOnTurn.TurnIndicatorAnimator.Play("turnIndicatorAppear", 0, 0);

        newTurnTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"{member.MemberName}'s turn";
        newTurnTextGameObject.GetComponent<Animator>().Play("turnText", 0, 0);
    }
    public void NewTurnEffectEnd()
    {
        GameManager.Instance.MemberToPlayTurn();
    }
    public void TargetArrows()
    {
        // get current targetable members
        var currentTargetables = GameManager.Instance.Members
            .Where(m => m.Targetable)
            .ToList();

        //  became targetable  appear
        foreach (var m in currentTargetables.Except(prevTargetables))
        {
            m.TargetedArrowAnimator.Play("targetArrowAppear");
        }
          

        //  stopped being targetable  disappear
        foreach (var m in prevTargetables.Except(currentTargetables))
        {
            m.TargetedArrowAnimator.Play("targetArrowDisappear");
        }

        // remember for next call
        prevTargetables = currentTargetables;
    }
}
