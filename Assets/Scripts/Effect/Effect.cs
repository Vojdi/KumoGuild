public class Effect
{
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
    public virtual string InfoBoxSyntax(int rounds, int value)
    {
        return null;
    }
}
