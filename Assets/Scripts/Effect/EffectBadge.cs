using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectBadge : MonoBehaviour
{
    EffectBadgeManager manager;
    private void Start()
    {
        manager = transform.parent.GetComponent<EffectBadgeManager>();
    }
    private void OnMouseEnter()
    {
        if (ControlPanel.Instance.ableToCheckEffects)
        {
            manager.MoveToPosition(gameObject);
            StartCoroutine(manager.Activate(gameObject));
        }
    }
   
    private void OnMouseExit()
    {
        Debug.Log("Exited");
        ControlPanel.Instance.EffectPanel.gameObject.SetActive(false);
    }
}
