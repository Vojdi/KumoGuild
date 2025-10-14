using UnityEngine;

public class NewTurnTextEventBridge : MonoBehaviour
{
    void EffectEnded()
    {
        VisualEffectManager.Instance.NewTurnEffectEnd();
    }
}
