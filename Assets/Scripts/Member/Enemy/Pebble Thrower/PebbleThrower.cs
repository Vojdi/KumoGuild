using UnityEngine;
public class PebbleThrower : EnemyMember
{
    protected override void Awake()
    {
        maxHealth = 8;
        speed = 5;
        position = 6;
        base.Awake();
        skills.Add(ScriptableObject.CreateInstance<FlyingPebble>());
        skills.Add(ScriptableObject.CreateInstance<Scattershot>());
    }
}
