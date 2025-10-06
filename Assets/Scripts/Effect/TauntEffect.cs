using System.Linq;
using UnityEngine;

public class TauntEffect : Effect
{
    public override void Attach()
    {
        foreach (var m in GameManager.Instance.Members)
        {
            if ((member is EnemyMember && m is EnemyMember) || (member is AllyMember && m is AllyMember))
            {
                if (m.Effects.OfType<TauntEffect>().Any())
                {
                    Debug.Log($"Taunt was removed from {m.name}, because only one member per side can have it");
                    m.Effects.OfType<TauntEffect>().First().EffectDied();
                }
            }
        }
        Debug.Log($"{member} taunted the opposite team");
        base.Attach();    
    }
    public override void EffectDied()
    {
        Debug.Log($"{member} does no longer taunt the opposite team");
        base.EffectDied();
    }
}
