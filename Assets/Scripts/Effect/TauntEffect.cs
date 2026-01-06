using System.Linq;
using UnityEngine;

public class TauntEffect : Effect
{
    public TauntEffect(int roundsLasts, int effectValue) : base(roundsLasts, effectValue) { }
    public override void Attach(Member m, int instanceId)
    {
        foreach (var me in GameManager.Instance.Members)
        {
            if ((m is EnemyMember && me is EnemyMember) || (m is AllyMember && me is AllyMember))
            {
                if (me.Effects.OfType<TauntEffect>().Any())
                {
                    Debug.Log($"Taunt was removed from {me.name}, because only one member per side can have it");
                    me.Effects.OfType<TauntEffect>().First().EffectDied();
                }
            }
        }
        Debug.Log($"{m} taunted the opposite team");
        base.Attach(m, InstanceId);    
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} does no longer taunt the opposite team");
        base.EffectDied();
    }
    public override string InfoBoxSyntax(int rounds, int value)
    {
        return $"taunt - {rounds}t";
    }
}
