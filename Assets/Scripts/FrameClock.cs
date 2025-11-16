using UnityEngine;

public class FrameClock : MonoBehaviour
{
    TMPro.TMP_Text tmp;
    int value;
    private void Awake()
    {
        tmp = GetComponent<TMPro.TMP_Text>();
        value = 1;
    }
    void Tick()
    {
        tmp.text = value.ToString();
        value++;
    }
}
