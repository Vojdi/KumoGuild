using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EffectBadgeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> badgeTypes;
    List<GameObject> instantiatedBadgeTypes;
    List<Type> effectTypes = new List<Type> {typeof(DoTEffect), typeof(ProtEffect), typeof(StunEffect), typeof(StunResistEffect), typeof(TauntEffect)};
    Vector3[] localBadgePositions = new Vector3[] {new Vector3(-0.12f,0,0), new Vector3(0.26f, 0, 0), new Vector3(0.64f, 0, 0), new Vector3(1.02f, 0, 0) };
    public Queue<Action> EffectBadgeQueue;
    public bool MidAnimation;


    public Member Member;

    

    List<Color32> effectTextColors = new List<Color32> {new Color32(135,3,220,255), new Color32(0, 133, 255, 255), new Color32(255, 25, 0, 255), new Color32(255, 136, 0, 255), new Color32(193, 21, 0, 255) };
    List<string> effectTextNames = new List<string> { "DoT", "Protection", "Stun", "Stun Resistance", "Taunt" };
    private void Awake()
    {
        instantiatedBadgeTypes = new List<GameObject>();
        EffectBadgeQueue = new Queue<Action>();
        MidAnimation = false;
    }
    private void Start()
    {
        Member = transform.parent.transform.parent.GetComponent<Member>(); 
        PreInstantiateEffects();
    }
    public void UpdateEffects(Type type, bool remove)
    {   
        MidAnimation = true;
        if (remove)
        {
            if(CheckForTypeCount(type) > 0)
            {
                FlashEffect(type);
            }
            else
            {
                DisappearEffect(type);
            }
        }
        else
        {
            if(CheckForTypeCount(type) > 1)
            {
                FlashEffect(type);
            }
            else
            {
                AppearEffect(type);
            }
        }

    }
    int CheckForTypeCount(Type type)
    {
        int count = 0;  
        foreach(var eff in Member.Effects)
        {
            if(eff.GetType() == type)
            {
                count++;
            }
        }
        return count;
    }
    int CheckForEnabledCount()
    {
        int count = 0;
        foreach(var instant in instantiatedBadgeTypes)
        {
            if (instant.activeSelf)
            {
                count++;
            }
        }
        return(count);
    }
    void AppearEffect(Type type)
    {
        var anim = instantiatedBadgeTypes[effectTypes.IndexOf(type)].GetComponent<Animator>();
        anim.transform.localPosition = localBadgePositions[CheckForEnabledCount()];
        anim.gameObject.SetActive(true);
        anim.Play("app",0,0);
    }
    void DisappearEffect(Type type)
    {
        var anim = instantiatedBadgeTypes[effectTypes.IndexOf(type)].GetComponent<Animator>();
        anim.Play("dapp",0,0);
    }
    public void DisappearEffectEnded(GameObject badge)
    {
        badge.SetActive(false);
        FixPlacement();
        MidAnimation = false;
        Debug.Log(EffectBadgeQueue.Count + "Count");
        EffectBadgeQueue.Dequeue().Invoke();
        Debug.Log("done");
    }
    public void EffectEnded()
    {
        MidAnimation = false;
        Debug.Log(EffectBadgeQueue.Count + "Count");
        
        EffectBadgeQueue.Dequeue().Invoke();
        
        Debug.Log("done");
    }
    void FixPlacement()
    {
        var activeBadges = instantiatedBadgeTypes.Where(b => b.activeSelf).ToList();
        for (int i = 0; i < activeBadges.Count; i++)
        {
             activeBadges[i].transform.localPosition = localBadgePositions[i];
        }
    } 
   
    public void FlashEffect(Type type)
    {
        instantiatedBadgeTypes[effectTypes.IndexOf(type)].GetComponent<Animator>().Play("flash",0,0);
    }
    void PreInstantiateEffects()
    {
        foreach (var badge in badgeTypes)
        {
            instantiatedBadgeTypes.Add(Instantiate(badge, this.transform));
            
        }
        foreach(var instantiatedBadge in instantiatedBadgeTypes)
        {
            instantiatedBadge.SetActive(false);
        }
    }
    public void AssingValuesToEffectPanel(GameObject badge)  
    {
        var index = instantiatedBadgeTypes.IndexOf(badge);
        var transformm = ControlPanel.Instance.EffectPanel.transform;
        var text = transformm.GetChild(0).GetComponent<TMPro.TMP_Text>();
        text.color = effectTextColors[index];
        text.text = effectTextNames[index];

        Type effectType = effectTypes[index];
        Effect[] effects = Member.Effects.Where(e => e.GetType() == effectType).ToArray();


        for(int i = 0;i < transformm.childCount - 1; i++)
        {
            transformm.GetChild(i + 1).gameObject.SetActive(false);
        }
        for (int i = 0; i < effects.Count(); i++)
        {
            var child = transformm.GetChild(i + 1);
            child.gameObject.SetActive(true);
            var childtmp = child.GetComponent<TMPro.TMP_Text>();
            childtmp.text = effects[i].InfoBoxSyntax(effects[i].RoundsLast, effects[i].EffectValue);
        }
    }
    public IEnumerator Activate(GameObject badgeGameObject)
    {
        var cg = ControlPanel.Instance.EffectPanel.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        AssingValuesToEffectPanel(badgeGameObject);
        ControlPanel.Instance.EffectPanel.gameObject.SetActive(true);
        yield return null; 
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            ControlPanel.Instance.EffectPanel.GetComponent<RectTransform>()
        );
        Destroy(cg);
    }
    public void MoveToPosition(GameObject badgeGameObject)
    {
        Vector3 worldPos = badgeGameObject.transform.position + new Vector3(0, 0.3f, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransform rect = ControlPanel.Instance.EffectPanel.GetComponent<RectTransform>();
        rect.position = screenPos;
    }
}
