using UnityEngine;

public class turnIndicatorStates : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Appeared()
    {
        animator.Play("turnIndicatorIdle",0,0);
    }
}
