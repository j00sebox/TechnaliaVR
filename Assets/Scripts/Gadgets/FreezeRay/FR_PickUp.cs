using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FR_PickUp : PickUp
{
    public int index = 0;
    public GadgetManager gm;

    public override void pick_up_action() 
    {
        pickupTxt.enabled = false;
        gm.SpawnFR();
        GadgetManager.gadgets_obtained[index] = true;
    }
}
