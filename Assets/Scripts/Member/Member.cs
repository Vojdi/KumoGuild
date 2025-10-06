using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Member : MonoBehaviour
{
    public string MemberName;
    protected int maxHealth;
    public int MaxHealth => maxHealth;

    protected int health;
    public int Health => health;

    protected int speed;
    public int Speed => speed;
    public int Position;

    public List<Skill> Skills;
    public List<Effect> Effects;
    public bool Targetable;
    protected bool stunnedThisRound;
    protected virtual void Awake()
    {
        Skills = new List<Skill>();
        Effects = new List<Effect>();
        health = maxHealth;
        Targetable = false;
    }
    public virtual void YourTurn()
    {
        stunnedThisRound = false;
        if (Effects.OfType<StunEffect>().Any())
        {
            var stun = Effects.OfType<StunEffect>().First();
            Debug.Log($"{this} is skipping this turn due to Stun");
            stun.EffectAbsorbed();
            stunnedThisRound = true;
            GameManager.Instance.NextTurn();
                
        }
    }
    public void Damage(int damage)
    {
        Debug.Log($"{this} got Attacked for {damage} damage");
        health -= damage;
        if (health <= 0) 
        {
            Debug.Log($"{this} died");
            GameManager.Instance.MemberDied(this);
        }
    }
    public void EffectsTime()
    {
        for (int i = Effects.Count - 1; i >= 0; i--)
        {
            if (Effects[i] is Effect eff && (eff is StunResistEffect || eff is DoTEffect))
            {
                eff.EffectAbsorbed();
            }
        }
    }
    private void OnMouseDown()
    {
        if (Targetable)
        {
            ControlPanel.Instance.SkillPositionSelected(Position);
        }
    }
}
