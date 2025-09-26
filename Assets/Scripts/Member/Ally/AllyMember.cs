using System.Collections.Generic;
using UnityEngine;

public class AllyMember : Member
{
    public override void YourTurn()
    {
        ControlPanel.Instance.AllyTurn(this); 
    }
}
