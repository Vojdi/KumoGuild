using UnityEngine;

public class HpBadge : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text hpText;
    Member member;
    public void Init(Member m)
    {
        member = m;
        Debug.Log(member);
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        hpText.text = member.Health.ToString();
        if (member.Health > 99)
        {
            hpText.fontSize = 2;
        }
        else
        {
            hpText.fontSize = 3;
        }
       
    }
}
