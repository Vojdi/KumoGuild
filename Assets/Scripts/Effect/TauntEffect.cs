using System.Linq;
using UnityEngine;

public class TauntEffect : Effect
{
    public override void Attach(Member member)
    {
        foreach (var m in GameManager.Instance.Members.Where(m => m.GetType() == member.GetType()))
        {
            if (m.Effects.OfType<TauntEffect>().Any())
            {
                Debug.Log($"Taunt was removed from {m.name}, because only one member per side can have it");
                m.Effects.OfType<TauntEffect>().First().EffectDied();
            }
        }
        Debug.Log($"{member} taunted the opposite team");
        base.Attach(member);    
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} does no longer taunt the opposite team");
        base.EffectDied();
    }
}
