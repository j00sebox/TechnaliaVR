using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_PickUp : PickUp
{

    public static bool isHoldingSpringBoots = false;
        

    public override void pick_up_action() 
    {
        GameObject.Find("Protagonist").GetComponent<PlayerMovement> ().springBoots = true;
        isHoldingSpringBoots = true;
    }
}
