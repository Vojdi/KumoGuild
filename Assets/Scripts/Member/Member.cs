using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Member : MonoBehaviour
{
    protected string memberName;
    public string MemberName => memberName;
    
    protected int maxHealth;
    public int MaxHealth => maxHealth;

    protected int health;
    public int Health => health;

    protected int speed;
    public int Speed => speed;
    [HideInInspector] public int Position;

    [HideInInspector] public List<Skill> Skills;
    [HideInInspector] public List<Effect> Effects;
    [HideInInspector] public bool Targetable;
    protected bool stunnedThisRound;
    [HideInInspector] public Animator TurnIndicatorAnimator;
    [HideInInspector] public Animator TargetedIndicatorAnimator;
    [HideInInspector] public Animator TargetedArrowAnimator;
    protected HpBadge hpBadge;
    SpriteRenderer highlightPanel;
    protected virtual void Awake()
    {
        Skills = new List<Skill>();
        Effects = new List<Effect>();
        health = maxHealth;
        Targetable = false;
        TurnIndicatorAnimator = GetComponentInChildren<TurnIndicatorStates>().gameObject.GetComponent<Animator>();
        TargetedIndicatorAnimator = GetComponentInChildren<TargetIndicatorStates>().gameObject.GetComponent<Animator>();
        TargetedArrowAnimator = GetComponentInChildren<TargetArrowStates>().gameObject.GetComponent<Animator>();
        hpBadge = GetComponentInChildren<HpBadge>();
    }
    private void Start()
    {
        hpBadge.Init(this);
        highlightPanel = transform.Find("Highlight").GetComponent<SpriteRenderer>();
    }
    public virtual void YourTurn()
    {
        stunnedThisRound = false;
        if (Effects.OfType<StunEffect>().Any())
        {
            var stun = Effects.OfType<StunEffect>().First();
            stun.EffectAbsorbed();
            Debug.Log($"{this} is skipping this turn due to Stun");
            stunnedThisRound = true;
            TurnIndicatorAnimator.Play("disappear");
            GameManager.Instance.NextTurn();
        }
    }
    public void Damage(int damage, bool protectionBypass)
    {
        if(damage < 0)
        {
            if (health - damage > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health -= damage;
            }
            VisualEffectManager.Instance.ActionQueue.Enqueue(hpBadge.UpdateHealth);
        }
        else if (protectionBypass)
        {
            Debug.Log($"{this} got Damaged by DoT for {damage} damage");
            health -= damage;
            if(health > 0)
            {
                VisualEffectManager.Instance.ActionQueue.Enqueue(hpBadge.UpdateHealth);
            }
            if (health <= 0)
            {
                Debug.Log($"{this} died");
                GameManager.Instance.MemberDied(this);
            }
        }
        else
        {
            int protection = 0;
            foreach (Effect eff in Effects)
            {
                if (eff is ProtEffect protEff)
                    protection += protEff.EffectValue;
            }
            if(protection > 100)
            {
                protection = 100;
            }
            int finalDamage = damage - Mathf.RoundToInt(damage / 100f * protection);
            Debug.Log($"{this} got Damaged for {finalDamage} damage,he had {protection}% prot, base damage was {damage}");
            health -= finalDamage;
            if(health > 0)
            {
                VisualEffectManager.Instance.ActionQueue.Enqueue(hpBadge.UpdateHealth);
            }
            
            if (health <= 0)
            {
                Debug.Log($"{this} died");
                GameManager.Instance.MemberDied(this);
            }
        }
    }
    public void EffectsTime()
    {
        int dotVal = 0;
        foreach (var effect in Effects.ToList())
        {
            if(effect is DoTEffect)
            {
                dotVal += effect.EffectValue;
            }
            if(!(effect is StunEffect))
            {
                effect.EffectAbsorbed();
            }
        }
        if(dotVal > 0)
        {
            Damage(dotVal, true);
        }
    }
    private void OnMouseDown()
    {
        if (Targetable)
        {
            ControlPanel.Instance.SkillPositionSelected(Position);
            Color32 c = highlightPanel.color;
            highlightPanel.color = new Color32(c.r, c.g, c.b, 0);
        }
    }
    private void OnMouseEnter()
    {
        if (Targetable) {
            Color32 c = highlightPanel.color;
            highlightPanel.color = new Color32(c.r, c.g, c.b, 15);
        }
       
    }
    private void OnMouseExit()
    {
        Color32 c = highlightPanel.color;
        highlightPanel.color = new Color32(c.r, c.g, c.b, 0);
    }
    public void Die()
    {
        Destroy(hpBadge.transform.parent.gameObject);
        Destroy(GetComponent<Animator>()); 
        GetComponent<SpriteRenderer>().sprite = ImageManager.Instance.GetSprite(10);
    }
}
