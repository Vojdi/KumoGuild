using UnityEngine;

public class Knight : AllyMember
{
    protected override void Awake()
    {
        maxHealth = 15;
        speed = 4;
        position = 1;// 
        base.Awake();
        skills.Add(ScriptableObject.CreateInstance<GuidedSlash>());
        skills.Add(ScriptableObject.CreateInstance<PiercingLight>());
    }
}
