using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    static StoryScene instance;
    public static StoryScene Instance => instance;

    [SerializeField] Animator storyTextAnimator;
    [SerializeField] TMPro.TMP_Text storyText;
    [SerializeField] TMPro.TMP_Text skipContinueText;
    [SerializeField] Animator PressKeyAnimator;
    [SerializeField] String[] storyTexts;
    [SerializeField] AudioSource source;

    Queue<string> storyTextsQueue;
    bool skippable;

    private void Awake()
    {
        storyTextsQueue = new Queue<string>();
        instance = this;
        skippable = false;
        foreach (var storyText in storyTexts)
        {
            storyTextsQueue.Enqueue(storyText);
        }
    }
    private void Start()
    {
        float ppa = PlayerPrefs.GetFloat("audio");
        if (ppa != 0)
        {
            source.volume = ppa;
        }
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
    public void AppearPressAnyKeyText()
    {
        PressKeyAnimator.Play("pressAnyKeyAppear", 0, 0);
    }
    public void NextText()
    {
        if (storyTextsQueue.Count > 0) {
            storyText.text = storyTextsQueue.Dequeue(); 
            storyTextAnimator.Play("textAppear", 0, 0);
        }
        else
        {
            skipContinueText.text = "Press any key to continue";
            PressKeyAnimator.Play("pressAnyKeyIdle");
        }
    }
    public void EnableSkip()
    {
        skippable = true;
    }
}
