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
    public override void Attach(Member m, int instanceId)
    {
        InstanceId = instanceId;
        foreach (Effect eff in m.Effects)
        {
            if (eff is ProtEffect protEff)
            {
                if (InstanceId == protEff.InstanceId)
                {
                    protEff.roundsLasts += roundsLasts;
                    Debug.Log($"{m}'s prot got prolonged (effect stacking)");
                    return;
                }
            }
        }
        Debug.Log($"{m} got prot");
        base.Attach(m, instanceId);
    }
    public override string InfoBoxSyntax(int rounds, int value, bool effectBox)
    {
        if (effectBox)
        {
            return $"{value}% / {rounds} rounds";
        }
        else
        {
            return $"prot - {value}% / {rounds}r";
        }
    }
}
