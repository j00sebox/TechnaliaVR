using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_PickUp : PickUp
{
    public override void pick_up_action(Transform player) 
    {
        player.GetComponent<PlayerMovement> ().springBoots = true;
    }
}
