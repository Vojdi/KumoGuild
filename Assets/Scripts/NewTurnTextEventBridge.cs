using UnityEngine;

public class NewTurnTextEventBridge : MonoBehaviour
{
    public void EffectEnded()
    {
        VisualEffectManager.Instance.NextTurnEffectEnd();
    }
}
