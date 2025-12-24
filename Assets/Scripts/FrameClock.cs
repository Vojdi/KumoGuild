using System;
using System.Collections.Generic;
using UnityEngine;

public class FrameClock : MonoBehaviour
{
    static FrameClock instance;
    public static FrameClock Instance => instance;
    public List<Action> AnimationActions;
    List<Action> animationsToRemove;
    private void Awake()
    {
        instance = this;
        AnimationActions = new List<Action>();
        animationsToRemove = new List<Action>();
    }
    void Tick()
    {
        foreach (var action in AnimationActions)
        {   
                action.Invoke();
                animationsToRemove.Add(action);
        }
        foreach (var action in animationsToRemove)
        {
            AnimationActions.Remove(action); 
        }
        animationsToRemove.Clear();
    }
}
