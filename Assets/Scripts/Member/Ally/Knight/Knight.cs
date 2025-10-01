using UnityEngine;

public class Knight : AllyMember
{
    protected override void Awake()
    {
        MemberName = "Knight";
        maxHealth = 15;
        speed = 4;
        position = 1;// 
        base.Awake();
        Skills.Add(ScriptableObject.CreateInstance<GuidedSlash>());
        Skills.Add(ScriptableObject.CreateInstance<PiercingLight>());
    }
}
