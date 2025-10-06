public class Effect
{
    protected Member member;
    protected int roundsLasts;
    public int RoundsLast => roundsLasts;
    public virtual void EffectAbsorbed()
    {
        roundsLasts--;
        if(roundsLasts <= 0)
        {
            EffectDied();
        }
    }
    public virtual void EffectDied()
    {
        member.Effects.Remove(this);
    }
    public virtual void SetValues(Member member, int roundsLasts)
    {
        this.member = member;
        this.roundsLasts = roundsLasts;
    }
    public virtual void Attach() { 
        member.Effects.Add(this);
    }
}
