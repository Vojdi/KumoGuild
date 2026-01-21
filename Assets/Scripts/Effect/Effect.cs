
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Effect
{
    public int InstanceId;
    protected Member member;
    protected int roundsLasts;
    protected int effectValue;
    public int RoundsLast => roundsLasts;
    public int EffectValue => effectValue;  
    public Effect(int roundsLasts, int effectValue)
    {
        this.roundsLasts = roundsLasts;
        this.effectValue = effectValue;
    }
    public virtual void EffectAbsorbed()
    {
        Debug.Log($"{this} + flashes");
        roundsLasts--;
        if(roundsLasts <= 0)
        {
            EffectDied();
        }
        else
        {
            if (!member.EffectBadgeManager.MidAnimation)
            {
                member.EffectBadgeManager.FlashEffect(this.GetType());
            }
            else 
            {
                member.EffectBadgeManager.EffectBadgeQueue.Enqueue(() => member.EffectBadgeManager.FlashEffect(this.GetType()));
            }
        }
    }
    public virtual void EffectDied()
    {
        member.Effects.Remove(this);
        if (!member.EffectBadgeManager.MidAnimation)
        {
            member.EffectBadgeManager.UpdateEffects(this.GetType(), true);
        }
        else
        {
            member.EffectBadgeManager.EffectBadgeQueue.Enqueue(() => member.EffectBadgeManager.UpdateEffects(this.GetType(), true));
        }
    }
    public virtual void Attach(Member m, int instanceId) 
    {
        member = m;
        InstanceId = instanceId;
        member.Effects.Add(this);
        if(member.EffectBadgeManager.EffectBadgeQueue.Count == 0 && !member.EffectBadgeManager.MidAnimation)
        {
            member.EffectBadgeManager.UpdateEffects(this.GetType(), false);
        }
        else
        {
            member.EffectBadgeManager.EffectBadgeQueue.Enqueue(() => member.EffectBadgeManager.UpdateEffects(this.GetType(), false));
        }
    }
    public virtual string InfoBoxSyntax(int rounds, int value, bool effectBox)
    {
        return null;
    }
}
