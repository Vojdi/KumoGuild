using UnityEngine;

public class DarkenOverlayEventBridge : MonoBehaviour
{
    public void Darkened()
    {
        VisualEffectManager.Instance.Darkened();
    }
    public void Lightened()
    {
        VisualEffectManager.Instance.Lightened();
    }
}
