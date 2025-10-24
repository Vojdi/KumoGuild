using UnityEngine;

public class StoryTextEventBridge : MonoBehaviour
{
   void Ended()
   {
        StoryScene.Instance.AppearPressAnyKeyText();
   }
}
