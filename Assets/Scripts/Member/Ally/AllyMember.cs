using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllyMember : Member
{
    public override void YourTurn()
    {
        Debug.Log($"It is {gameObject.name}'s turn");////
        ControlPanel.Instance.AllyTurn(this); 
    }
   
}
