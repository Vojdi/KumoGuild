using UnityEngine;

public class MainCameraEventBridge : MonoBehaviour
{
    void Ended()
    {
        StoryScene.Instance.AppearText();
    }
}
