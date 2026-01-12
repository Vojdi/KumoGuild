
using UnityEngine;

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
        Debug.Log($"{this} + dies");
        member.Effects.Remove(this);
        if (!member.EffectBadgeManager.MidAnimation)
        {
            Debug.Log($"{this} + dissapears");
            member.EffectBadgeManager.UpdateEffects(this.GetType(), true);
        }
        else
        {
            Debug.Log($"{this} + enqueue dissapears");
            member.EffectBadgeManager.EffectBadgeQueue.Enqueue(() => member.EffectBadgeManager.UpdateEffects(this.GetType(), true));
        }
    }
    public virtual void Attach(Member m, int instanceId) 
    {
        Debug.Log($"{this} + appears");
        member = m;
        InstanceId = instanceId;
        member.Effects.Add(this);
        if(member.EffectBadgeManager.EffectBadgeQueue.Count == 0 && !member.EffectBadgeManager.MidAnimation)
        {
            member.EffectBadgeManager.UpdateEffects(this.GetType(), false);
            Debug.Log($"{this} + appear");
        }
        else
        {
            Debug.Log($"{this} + enqueue appear");
            member.EffectBadgeManager.EffectBadgeQueue.Enqueue(() => member.EffectBadgeManager.UpdateEffects(this.GetType(), false));
        }
       
    }
    public virtual string InfoBoxSyntax(int rounds, int value)
    {
        return null;
    }
}
