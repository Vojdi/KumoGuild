using UnityEngine;

public class Thug : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 11;
        speed = 3;
        position = 4;//
        base.Awake();
        skills.Add(ScriptableObject.CreateInstance<ClumsyStrike>());
    }
}
