using Unity.VisualScripting;
using UnityEngine;

public class ProtEffect : Effect
{
    public int protectionValue;
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer Affected by this Prot");
        base.EffectDied();
    }
    public void SetValues(Member member, int roundsLasts, int protectionValue)
    {
        this.member = member;
        this.roundsLasts = roundsLasts;
        this.protectionValue = protectionValue;
    }
    public override void Attach()
    {
        foreach (Effect eff in member.Effects)
        {
            if (eff is ProtEffect protEff)
            {
                if (protectionValue == protEff.protectionValue)
                {
                    protEff.roundsLasts += roundsLasts;
                    Debug.Log($"{member}'s prot got prolonged (effect stacking)");
                    return;
                }
            }
        }
        Debug.Log($"{member} got prot");
        base.Attach();
    }
}
