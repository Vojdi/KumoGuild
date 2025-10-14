using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] Animator darkenOverlayAnimator;
    [SerializeField] GameObject newTurnTextGameObject;
    [SerializeField] GameObject skillUsedTextGameObject;
    static VisualEffectManager instance;
    public static VisualEffectManager Instance => instance;
    Member memberOnTurn;

    Animator animator;
    Skill attackingSkill;
    int attackingPos;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    public void PlayEffectAnimation(Skill skill,int pos)
    {
        attackingSkill = skill;
        attackingPos = pos;
        skillUsedTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"Skill: {skill.SkillName}";
        if(memberOnTurn is EnemyMember)
        {
            skillUsedTextGameObject.GetComponent<Animator>().Play("enemyUsedSkill", 0, 0);
        }
        else
        {
            skillUsedTextGameObject.GetComponent<Animator>().Play("allyUsedSkill", 0, 0);
        }
    }
    public void SkillAnncounced()
    {
        memberOnTurn.TurnIndicatorAnimator.Play("New State");
        darkenOverlayAnimator.Play("darken", 0, 0);
    }
    public void Darkened()
    {
        animator.Play(attackingSkill.AnimName, 0, 0);
    }
    public void Lightened()
    {
        attackingSkill.UseSkill(attackingPos);
        GameManager.Instance.NextTurn();
    }
    public void EffectEnded()
    {
        darkenOverlayAnimator.Play("lighten", 0, 0);
    }
    public void NewTurnEffect(Member member)
    {
        memberOnTurn = member;
        memberOnTurn.TurnIndicatorAnimator.Play("turnIndicatorIdle",0,0);

        newTurnTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"{member.MemberName}'s turn";
        newTurnTextGameObject.GetComponent<Animator>().Play("turnText", 0, 0);
    }
    public void NewTurnEffectEnd()
    {
        GameManager.Instance.MemberToPlayTurn();
    }
}
