using UnityEngine;

public class DarkenOverlayEventBridge : MonoBehaviour
{
    void Action()
    {
        VisualEffectManager.Instance.ActionQueue.Dequeue()?.Invoke();
    }
}
