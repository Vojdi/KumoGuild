using UnityEditor.Tilemaps;
using UnityEngine;

public class DoTEffect : Effect
{
    int damageOverTime;
    public DoTEffect(int roundsLasts, int damageOverTime) : base(roundsLasts)
    {
        this.damageOverTime = damageOverTime;
    }
   
    public override void EffectAbsorbed()
    {
        member.Damage(damageOverTime,true);
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
                dotEff.damageOverTime += damageOverTime;
                Debug.Log($"{m}'s DoT got stacked");
                return;
            }       
        }
        Debug.Log($"{m} got DoT");
        base.Attach(m);
    }
}
