using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class EffectBadgeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> badgeTypes;
    List<GameObject> instantiatedBadgeTypes;
    List<Type> effectTypes = new List<Type> {typeof(DoTEffect), typeof(ProtEffect), typeof(StunEffect), typeof(StunResistEffect), typeof(TauntEffect)};
    Vector3[] localBadgePositions = new Vector3[] {new Vector3(-0.12f,0,0), new Vector3(0.26f, 0, 0), new Vector3(0.64f, 0, 0), new Vector3(1.02f, 0, 0) };
  
    Member member;

    private void Awake()
    {
        instantiatedBadgeTypes = new List<GameObject>();
    }
    private void Start()
    {
        member = transform.parent.transform.parent.GetComponent<Member>(); 
        PreInstantiateEffects();
    }
    public void UpdateEffects(Type type, bool remove)
    {
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
        foreach(var eff in member.Effects)
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
        anim.Play("app");
    }
    void DisappearEffect(Type type)
    {
        var anim = instantiatedBadgeTypes[effectTypes.IndexOf(type)].GetComponent<Animator>();
        anim.Play("dapp");
    }
    public void DisappearEffectEnded(GameObject badge)
    {
        badge.SetActive(false);
        FixPlacement();
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
        instantiatedBadgeTypes[effectTypes.IndexOf(type)].GetComponent<Animator>().Play("flash");
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
}
