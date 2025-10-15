using UnityEngine;

public class DarkenOverlayEventBridge : MonoBehaviour
{
    void Darkened()
    {
        VisualEffectManager.Instance.Darkened();
    }
    void Lightened()
    {
        VisualEffectManager.Instance.Lightened();
    }
}
