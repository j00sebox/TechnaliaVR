using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlot : MonoBehaviour
{

    public GameObject Gadget { get; set; }

    public void AttachToSlot(GameObject gadget)
    {
        Gadget = gadget;

        Gadget.transform.position = gameObject.transform.position;

        Gadget.transform.SetParent(transform);

        Gadget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Gadget.GetComponent<InteractableGadget>().slotRef = this;
    }

    void OnTriggerEnter(Collider coll)
    {

        if(Gadget == null && coll.tag == "Gadget" && !coll.gameObject.GetComponent<InteractableGadget>().held)
        {
            AttachToSlot(coll.gameObject);
        }
    }

    public void ReleaseGadget()
    {

        Gadget = null;

    }
}
