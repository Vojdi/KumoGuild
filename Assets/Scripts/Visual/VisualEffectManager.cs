using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] Animator darkenOverlayAnimator;
    [SerializeField] GameObject newTurnTextGameObject;
    [SerializeField] GameObject skillUsedTextGameObject;
    static VisualEffectManager instance;
    public static VisualEffectManager Instance => instance;
    public Queue<Action> ActionQueue;
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
        ActionQueue = new Queue<Action>();
    }
    public void PlayEffectAnimation(Skill skill, int pos)
    {
        ActionQueue.Enqueue(SkillAnncounced);
        ActionQueue.Enqueue(Darkened);
        ActionQueue.Enqueue(Lightened);

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

        if (skill.SkillRangeType == "multi")
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
            m.TargetedIndicatorAnimator.Play("appear",0,0);
        }
    }
    public void SkillAnncounced()
    {
        FrameClock.Instance.AnimationActions.Add(() =>memberOnTurn.TurnIndicatorAnimator.Play("disappear"));
        foreach(Member m in targetMembers)
        {
            FrameClock.Instance.AnimationActions.Add(() => m.TargetedIndicatorAnimator.Play("disappear"));
        }
        ControlPanel.Instance.AbleToCheckEffects = false;
        if (ControlPanel.Instance.EffectPanel.activeSelf)
        {
            ControlPanel.Instance.EffectPanel.SetActive(false);
        }
        darkenOverlayAnimator.Play("darken", 0, 0);
    }
    void Darkened()
    {
        animator.Play(attackingSkill.AnimName, 0, 0);
    }
    void Lightened()
    {
        attackingSkill.UseSkill(attackingPos);
        ActionQueue.Enqueue(GameManager.Instance.NextTurn);
        ActionQueue.Dequeue()?.Invoke();
    }
    void EffectEnded()
    {
        darkenOverlayAnimator.Play("lighten", 0, 0);
    }
    public void NewTurnEffect(Member member)
    {
        ActionQueue.Enqueue(GameManager.Instance.MemberToPlayTurn);

        memberOnTurn = member;
        memberOnTurn.TurnIndicatorAnimator.Play("appear", 0, 0);

        newTurnTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"{member.MemberName}'s turn";
        newTurnTextGameObject.GetComponent<Animator>().Play("turnText", 0, 0);
    }
    public void NewRound(int count)
    {
        newTurnTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"Round {count}";
        newTurnTextGameObject.GetComponent<Animator>().Play("turnText", 0, 0);
    }
    public void TargetArrows()
    {
        var currentTargetables = GameManager.Instance.Members
            .Where(m => m.Targetable)
            .ToList();

        foreach (var m in currentTargetables.Except(prevTargetables))
        {
            FrameClock.Instance.AnimationActions.Add(() => m.TargetedArrowAnimator.Play("targetArrowAppear", 0, 0));
        }
          
        foreach (var m in prevTargetables.Except(currentTargetables))
        {
            FrameClock.Instance.AnimationActions.Add(() => m.TargetedArrowAnimator.Play("targetArrowDisappear", 0, 0));
        }

        prevTargetables = currentTargetables;
    }
}
