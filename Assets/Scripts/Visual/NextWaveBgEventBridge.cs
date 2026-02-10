using UnityEngine;

public class NextWaveBgEventBridge : MonoBehaviour
{
    private void Load()
    {
        GameManager.Instance.NextWaveSpawn();
;    }
    private void Continue()
    {
       GameManager.Instance.NextWaveReady();
    }
}
