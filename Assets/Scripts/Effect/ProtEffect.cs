using Unity.VisualScripting;
using UnityEngine;

public class ProtEffect : Effect
{
    public ProtEffect(int roundsLasts, int effectValue): base(roundsLasts, effectValue) { }

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
                if (effectValue == protEff.EffectValue)
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
    public override string InfoBoxSyntax(int rounds, int value)
    {
        return $"prot - {value}% / {rounds}t";
    }
}
