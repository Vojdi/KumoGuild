using UnityEngine;

public class DoTEffect : Effect
{
    public DoTEffect(int roundsLasts, int effectValue) : base(roundsLasts, effectValue) { }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer affected by this DoT");
        base.EffectDied();
    }
    public override void Attach(Member m, int instanceId)
    {
        foreach (Effect eff in m.Effects) {
            if(eff is DoTEffect dotEff)
            if (roundsLasts == dotEff.roundsLasts)
            {
                dotEff.effectValue +=  effectValue;
                Debug.Log($"{m}'s DoT got stacked");
                return;
            }       
        }
        Debug.Log($"{m} got DoT");
        base.Attach(m, instanceId);
    }
    public override string InfoBoxSyntax(int rounds, int value, bool effectBox)
    {
        if (effectBox)
        {
            return $"{value} dmg / {rounds} rounds";
        }
        else
        {
            return $"dot - {value} / {rounds}r";
        }
            
    }
}
