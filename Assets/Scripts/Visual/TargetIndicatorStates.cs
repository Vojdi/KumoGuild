using UnityEngine;

public class TargetIndicatorStates : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Appeared()
    {
        animator.Play("targetIndicatorIdle", 0, 0);
    }
}
