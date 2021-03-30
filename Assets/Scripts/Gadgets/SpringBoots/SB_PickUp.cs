using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_PickUp : PickUp
{
    public override void pick_up_action() 
    {
        GameObject.Find("Protagonist").GetComponent<PlayerMovement> ().springBoots = true;
    }
}
