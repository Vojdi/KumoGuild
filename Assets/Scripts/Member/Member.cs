using UnityEngine;
using System.Collections.Generic;

public class Member : MonoBehaviour
{
    protected int maxHealth;
    public int MaxHealth => maxHealth;

    protected int health;
    public int Health => health;

    protected int speed;
    public int Speed => speed;
 
    protected int position;
    public int Position => position;

    protected List<Skill> skills;
    public List<Skill> Skills => skills;
    public bool Targetable;


    protected virtual void Awake()
    {
        skills = new List<Skill>();
        health = maxHealth;
        Targetable = false;
    }
    public virtual void YourTurn()
    {
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
    private void OnMouseDown()
    {
        if (Targetable)
        {
            ControlPanel.Instance.SkillPositionSelected(position);
        }
    }
    protected void CallNextTurn()
    {
        GameManager.Instance.NextTurn();
    }

}
