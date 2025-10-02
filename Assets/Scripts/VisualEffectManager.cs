using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
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
        animator.Play(skill.AnimName,0,0);
    }
    void EffectEnded()
    {
        attackingSkill.UseSkill(attackingPos);
        GameManager.Instance.NextTurn();
    }
}
