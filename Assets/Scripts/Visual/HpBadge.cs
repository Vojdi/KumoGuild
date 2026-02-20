using UnityEngine;

public class HpBadge : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text hpText;
    [SerializeField] TMPro.TMP_Text damagePopUpText;
    [SerializeField] TMPro.TMP_Text damagePopUpText2;
    Member member;
    Animator hpAnimator;
    Animator damagePopUpAnimator;
    Animator damagePopUpAnimator2;
    public void Init(Member m)
    {
        member = m;
        hpAnimator = GetComponent<Animator>();
        damagePopUpAnimator = damagePopUpText.gameObject.GetComponent<Animator>();
        damagePopUpAnimator2 = damagePopUpText2.gameObject.GetComponent<Animator>();
        if (member.Health > 99)
        {
            hpText.fontSize = 2;
        }
        else
        {
            hpText.fontSize = 3;
        }
        hpText.text = member.Health.ToString();
    }
    public void UpdateHealth(bool dot)
    {
        DmgPopUpPrep(dot);
        if (dot)
        {
            hpAnimator.Play("changedHpEffect", 0, 0);
            damagePopUpAnimator2.Play("pop", 0, 0);
        }
        else
        {
            hpAnimator.Play("changedHpEffect", 0, 0);
            damagePopUpAnimator.Play("pop", 0, 0);
        }
    }
    void DmgPopUpPrep(bool dot)
    {
        if (dot)
        {
            int lastHp = int.Parse(hpText.text);
            int newHp = member.Health;

            int hp = newHp - lastHp;
            if (hp > 0)
            {
                damagePopUpText2.text = $"+{hp}";
            }
            else
            {
                damagePopUpText2.text = $"{hp}";
            }
        }
        else
        {
            int lastHp = int.Parse(hpText.text);
            int newHp = member.Health;

            int hp = newHp - lastHp;
            if (hp > 0)
            {
                damagePopUpText.text = $"+{hp}";
            }
            else
            {
                damagePopUpText.text = $"{hp}";
            }
        }
           
    }
    void UpdateHealthEffectStart()
    {
        int fontSize;
        if (member.Health > 99)
        {
            fontSize = 2;
        }
        else
        {
            fontSize = 3;
        }
        fontSize += 2;
        hpText.fontSize = fontSize;
        hpText.text = member.Health.ToString();

    }
    private void UpdateHealthEffectEnd()
    {
        hpText.fontSize -= 2;
        VisualEffectManager.Instance.ActionQueue.Dequeue()?.Invoke();
    }
}
