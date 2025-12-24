using UnityEngine;

public class TurnIndicatorStates : MonoBehaviour
{
    [SerializeField] SpriteRenderer childSprite;
    SpriteRenderer thisSprite;
    private void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }
    void Appeared()
    {
        childSprite.color = new Color32(255, 255, 255, 255);
        thisSprite.color = new Color32(255, 255, 255, 0);
    }
    void StartedToDisappear()
    {
        thisSprite.color = new Color32(255, 255, 255, 255);
        childSprite.color = new Color32(255,255, 255, 0);
    }
}
