using System;
using System.Linq;
using UnityEngine;

public class StunEffect : Effect
{
    int startRoundLasts;
    public StunEffect(int roundsLasts) : base(roundsLasts)
    {
        startRoundLasts = roundsLasts;
    }
    public override void EffectDied()
    {
        base.EffectDied();
        Effect stunResEffect = new StunResistEffect(startRoundLasts * 2 + 1);
        Debug.Log($"{member} now has stun resistance for {startRoundLasts} turns");
        stunResEffect.Attach(member);
    }
    public override void Attach(Member m)
    {

        if (m.Effects.OfType<StunEffect>().Any() || m.Effects.OfType<StunResistEffect>().Any()) 
        {
            Debug.Log($"{m} couldn't be stunned" );
        }
        else
        {
            base.Attach(m);
            Debug.Log($"{m} was stunned");
        }
    }
}
