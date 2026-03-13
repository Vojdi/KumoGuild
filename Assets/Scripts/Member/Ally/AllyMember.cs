using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllyMember : Member
{
    [HideInInspector]public int IconId;
    [SerializeField] protected string memberDesc;
    public string MemberDesc => memberDesc;
    public override void YourTurn()
    {
        if (health > 0)
        {
            base.YourTurn();
            if (!stunnedThisRound)
            {
                Debug.Log($"It is {this}'s turn");////
                ControlPanel.Instance.AllyTurn(this);
            }  
        }
    }
}
