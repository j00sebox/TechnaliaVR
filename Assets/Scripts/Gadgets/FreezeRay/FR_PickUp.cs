using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FR_PickUp : PickUp
{
    public Transform dest;
    public Transform prefab;
    Transform t;

    public override void pick_up_action(Transform player) 
    {
        pickupTxt.enabled = false;
        t = Instantiate(prefab, dest.position, prefab.rotation);
        t.parent = GameObject.Find("GunHolder").transform;
        t.rotation = new Quaternion(0, 0, 0, 0);
    }
}
