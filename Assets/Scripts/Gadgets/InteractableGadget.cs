using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGadget : MonoBehaviour
{
    public bool held = false;

    public bool inSlot = false;

    public VRSlot slotRef;

    public void OnGrab() { 
        held = true; 

        if(slotRef)
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.SetParent(null);
            slotRef.ReleaseGadget();

            slotRef = null;

            inSlot = false;
        }
    }

    public void OnRelease() { 
        held = false; 
    }
}
