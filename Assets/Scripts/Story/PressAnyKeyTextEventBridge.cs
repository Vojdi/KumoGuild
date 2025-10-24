using UnityEngine;

public class PressAnyKeyTextEventBridge : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    void Appeared()
    {
        StoryScene.Instance.EnableSkip();
        animator.Play("pressAnyKeyIdle");
    }
}
