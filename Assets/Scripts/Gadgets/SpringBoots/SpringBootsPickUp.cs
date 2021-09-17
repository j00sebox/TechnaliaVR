using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpringBootsPickUp : ItemPickup
{
    protected override void PickUpAction(GameObject player) 
    {
        player.GetComponent<PlayerMovement> ().springBoots = true;

        Destroy(gameObject);
    }
}
