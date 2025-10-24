using UnityEngine;
public class PebbleThrower : EnemyMember
{
    protected override void Awake()
    {
        memberName = "Basic Enemy";
        maxHealth = 8;
        speed = 1;
        base.Awake();
        Skills.Add(ScriptableObject.CreateInstance<FlyingPebble>());
        Skills.Add(ScriptableObject.CreateInstance<Scattershot>());
    }
}
