using UnityEngine;

public class DoTEffect : Effect
{
    public DoTEffect(int roundsLasts, int effectValue) : base(roundsLasts, effectValue) { }
    public override void EffectAbsorbed()
    {
        member.Damage(effectValue,true);
        base.EffectAbsorbed();
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer affected by this DoT");
        base.EffectDied();
    }
    public override void Attach(Member m)
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
        base.Attach(m);
    }
    public override string InfoBoxSyntax(int rounds, int value)
    {
        return $"dot - {value} / {rounds}t";
    }
}
