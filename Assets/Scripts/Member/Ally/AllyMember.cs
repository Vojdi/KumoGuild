using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllyMember : Member
{
    public override void YourTurn()
    {
        if (health > 0)
        {
            base.YourTurn();
            if (!stunnedThisRound)
            {
                Debug.Log($"It is {gameObject.name}'s turn");////
                ControlPanel.Instance.AllyTurn(this);
            }  
        }
    }
}
