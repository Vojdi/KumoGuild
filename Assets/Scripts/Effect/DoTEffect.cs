using UnityEditor.Tilemaps;
using UnityEngine;

public class DoTEffect : Effect
{
    int damageOverTime;
    
    public void SetValues(Member member, int roundsLasts, int damageOverTime)
    {
        this.member = member;
        this.roundsLasts = roundsLasts;
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
    public override void Attach()
    {
        foreach (Effect eff in member.Effects) {
            if(eff is DoTEffect dotEff)
            if (damageOverTime == dotEff.damageOverTime)
            {
                dotEff.roundsLasts += roundsLasts;
                Debug.Log($"{member}'s DoT got prolonged(effect stacking)");
                return;
            }       
        }
        Debug.Log($"{member} got DoT");
        base.Attach();
    }
}
