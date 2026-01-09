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
        roundsLasts--;
        if(roundsLasts <= 0)
        {
            EffectDied();
        }
        else
        {
            member.EffectBadgeManager.FlashEffect(this.GetType());
        }
    }
    public virtual void EffectDied()
    {
        member.Effects.Remove(this);
        member.EffectBadgeManager.UpdateEffects(this.GetType(), true);
    }
    public virtual void Attach(Member m, int instanceId) 
    {
        member = m;
        InstanceId = instanceId;
        member.Effects.Add(this);
        member.EffectBadgeManager.UpdateEffects(this.GetType(),false);
    }
    public virtual string InfoBoxSyntax(int rounds, int value)
    {
        return null;
    }
}
