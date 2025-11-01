using UnityEngine;

public class StunResistEffect : Effect
{
    public StunResistEffect(int roundsLasts) : base(roundsLasts) { }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer immune to stun");
        base.EffectDied();
    }
}
