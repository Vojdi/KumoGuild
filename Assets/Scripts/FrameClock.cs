using System;
using System.Collections.Generic;
using UnityEngine;

public class FrameClock : MonoBehaviour
{
    static FrameClock instance;
    public static FrameClock Instance => instance;
    public Action AnimationAction;
    private void Awake()
    {
        AnimationAction = null;
    }
    void Tick()
    {
        if (AnimationAction != null)
        {
            AnimationAction.Invoke();
            AnimationAction = null;
        }
    }
}
