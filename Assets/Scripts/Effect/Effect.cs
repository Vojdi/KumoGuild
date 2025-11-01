public class Effect
{
    protected Member member;
    protected int roundsLasts;
    public int RoundsLast => roundsLasts;
    protected Effect(int roundsLasts)
    {
        this.roundsLasts = roundsLasts;
    }
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
    public virtual void Attach(Member m) 
    {
        member = m;
        member.Effects.Add(this);
    }
}
