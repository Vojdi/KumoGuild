using Unity.VisualScripting;
using UnityEngine;

public class ProtEffect : Effect
{
    public int protectionValue;
    public ProtEffect(int roundsLasts, int protectionValue) : base(roundsLasts)
    {
        this.protectionValue = protectionValue;
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer Affected by this Prot");
        base.EffectDied();
    }
    public override void Attach(Member m)
    {
        foreach (Effect eff in m.Effects)
        {
            if (eff is ProtEffect protEff)
            {
                if (protectionValue == protEff.protectionValue)
                {
                    protEff.roundsLasts += roundsLasts;
                    Debug.Log($"{m}'s prot got prolonged (effect stacking)");
                    return;
                }
            }
        }
        Debug.Log($"{m} got prot");
        base.Attach(m);
    }
}
