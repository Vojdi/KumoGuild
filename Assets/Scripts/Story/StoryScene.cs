using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    static StoryScene instance;
    public static StoryScene Instance => instance;

    [SerializeField] Animator storyTextAnimator;
    [SerializeField] Animator PressKeyAnimator;

    bool skippable;

    private void Awake()
    {
        instance = this;
        skippable = false;
    }
    private void Update()
    {
        if (skippable)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void AppearText()
    {
        storyTextAnimator.Play("textAppear",0,0);
    }
    public void AppearPressAnyKeyText()
    {
        PressKeyAnimator.Play("pressAnyKeyAppear", 0, 0);
    }

    public void EnableSkip()
    {
        skippable = true;
    }
}
