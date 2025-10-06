using System;
using System.Linq;
using UnityEngine;

public class StunEffect : Effect
{
    int startRoundLasts;
    public override void EffectDied()
    {
        base.EffectDied();
        Effect stunResEffect = new StunResistEffect();
        stunResEffect.SetValues(member, startRoundLasts * 2 + 1);
        Debug.Log($"{member} now has stun resistance for {startRoundLasts} turns");
        stunResEffect.Attach();
    }
    public override void SetValues(Member member, int roundsLasts)
    {
        base.SetValues(member, roundsLasts);
        startRoundLasts = roundsLasts;
    }
    public override void Attach()
    {
        if (member.Effects.OfType<StunEffect>().Any() || member.Effects.OfType<StunResistEffect>().Any()) 
        {
            Debug.Log($"{member} couldn't be stunned" );
        }
        else
        {
            base.Attach();
            Debug.Log($"{member} was stunned");
        }
    }
}
