using UnityEngine;

public class HpBadge : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text hpText;
    Member member;
    Animator hpAnimator;
    public void Init(Member m)
    {
        member = m;
        Debug.Log(member);
        hpAnimator = GetComponent<Animator>();
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
        hpAnimator.Play("changedHpEffect",0,0);
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
    }

}
