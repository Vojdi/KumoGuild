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
        member.Damage(damageOverTime);
        base.EffectAbsorbed();
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} is no longer affected by this DoT");
        base.EffectDied();
    }
    public override void Attach()
    {
        foreach (DoTEffect eff in member.Effects) {
            if (damageOverTime == eff.damageOverTime)
            {
                eff.roundsLasts += roundsLasts;
                Debug.Log($"{member}'s DoT got prolonged(effect stacking)");
                return;
            }       
        }
        Debug.Log($"{member} got DoT");
        base.Attach();
    }
}
