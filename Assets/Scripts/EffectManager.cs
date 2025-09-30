using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager instance;
    public static EffectManager Instance => instance;

    Animator animator;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    public void PlayEffectAnimation(string effName)
    {
        animator.Play(effName,0,0);
    }
    void EffectEnded()
    {
        GameManager.Instance.NextTurn();
    }
}
