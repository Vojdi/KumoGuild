using UnityEngine;

public class StunResistEffect : Effect
{
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer immune to stun");
        base.EffectDied();
    }
}
