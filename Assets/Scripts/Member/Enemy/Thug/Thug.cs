using UnityEngine;

public class Thug : EnemyMember
{
    protected override void Awake()
    {
        MemberName = "Thug";
        maxHealth = 11;
        speed = 3;
        position = 4;//
        base.Awake();
        Skills.Add(ScriptableObject.CreateInstance<ClumsyStrike>());
    }
}
