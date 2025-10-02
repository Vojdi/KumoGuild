using UnityEngine;
public class PebbleThrower : EnemyMember
{
    protected override void Awake()
    {
        MemberName = "Pebblethrower";
        maxHealth = 8;
        speed = 5;
        base.Awake();
        Skills.Add(ScriptableObject.CreateInstance<FlyingPebble>());
        Skills.Add(ScriptableObject.CreateInstance<Scattershot>());
    }
}
