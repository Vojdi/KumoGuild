using UnityEngine;

public class EffectBadgeEventBridge : MonoBehaviour
{
    GameObject badge;
    Member member;
    private void Start()
    {
        badge = gameObject;
        member = transform.parent.transform.parent.transform.parent.GetComponent<Member>();
    }
    void Disappeared()
    {
        member.EffectBadgeManager.DisappearEffectEnded(badge);
    }
}
