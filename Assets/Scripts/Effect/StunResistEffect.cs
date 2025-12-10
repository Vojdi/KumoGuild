using UnityEngine;

public class StunResistEffect : Effect
{
    public StunResistEffect(int roundsLasts, int effectValue) : base(roundsLasts, effectValue) { }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer immune to stun");
        base.EffectDied();
    }
    public override string InfoBoxSyntax(int rounds, int value)
    {
        return $"stunRes - {rounds}t";
    }
}
