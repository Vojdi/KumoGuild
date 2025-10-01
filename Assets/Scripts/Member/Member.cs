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
 
    protected int position;
    public int Position => position;

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
            Debug.Log($"{MemberName} is skipping this turn due to Stun");
            stun.EffectAbsorbed();
            stunnedThisRound = true;
            GameManager.Instance.NextTurn();
                
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            Debug.Log($"{gameObject.name} died");
            GameManager.Instance.Members.Remove(this);
            Destroy(gameObject);
        }
    }
    public void EffectsTime()
    {
        for (int i = Effects.Count - 1; i >= 0; i--)
        {
            if (Effects[i] is StunResistEffect stn)
            {
                stn.EffectAbsorbed();
            }
        }
    }
    private void OnMouseDown()
    {
        if (Targetable)
        {
            ControlPanel.Instance.SkillPositionSelected(position);
        }
    }
    
}
