using UnityEngine;

public class UsedSkillTextEventBridge : MonoBehaviour
{
    void EffectEnded()
    {
        VisualEffectManager.Instance.SkillAnncounced();
    }
}
