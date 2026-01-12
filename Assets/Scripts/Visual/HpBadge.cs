using UnityEngine;

public class HpBadge : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text hpText;
    [SerializeField] TMPro.TMP_Text damagePopUpText;
    Member member;
    Animator hpAnimator;
    Animator damagePopUpAnimator;
    public void Init(Member m)
    {
        member = m;
        Debug.Log(member);
        hpAnimator = GetComponent<Animator>();
        damagePopUpAnimator = damagePopUpText.gameObject.GetComponent<Animator>();
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
    public void UpdateHealth()
    {
        DmgPopUpPrep();
        hpAnimator.Play("changedHpEffect",0,0);
        damagePopUpAnimator.Play("pop", 0, 0);

    }
    void DmgPopUpPrep()
    {
        int lastHp = int.Parse(hpText.text);
        int newHp = member.Health;

        int hp = newHp - lastHp;
        if (hp > 0) {
            damagePopUpText.text = $"+{hp}";
        }
        else
        {
            damagePopUpText.text = $"{hp}";
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
