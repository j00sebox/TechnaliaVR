using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC_PickUp : PickUp
{
   
    public int index = 1;
    public GadgetManager gm;

    public override void pick_up_action() 
    {
        pickupTxt.enabled = false;
        gm.SpawnWC();
        GadgetManager.currentSelection = index;
        GadgetManager.gadgets_obtained[index] = true;
    }
}
