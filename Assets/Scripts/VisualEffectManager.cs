using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    static VisualEffectManager instance;
    public static VisualEffectManager Instance => instance;

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
