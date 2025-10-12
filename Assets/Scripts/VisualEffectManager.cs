using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] Animator darkenOverlayAnimator;
    [SerializeField] GameObject newTurnTextGameObject;
    static VisualEffectManager instance;
    public static VisualEffectManager Instance => instance;

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
    public void NextTurnEffect(Member member)
    {
        newTurnTextGameObject.GetComponent<TMPro.TMP_Text>().text = $"{member.MemberName}'s turn";
        newTurnTextGameObject.GetComponent<Animator>().Play("turnText", 0, 0);
    }
    public void NextTurnEffectEnd()
    {
        GameManager.Instance.MemberToPlayTurn();
    }
}
