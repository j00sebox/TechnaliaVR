using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlot : MonoBehaviour
{
    private GameObject _gadget;

    void OnTriggerEnter(Collider coll)
    {

        if(_gadget == null && coll.tag == "Gadget" && !coll.gameObject.GetComponent<InteractableGadget>().held)
        {

            _gadget = coll.gameObject;

            _gadget.transform.SetParent(transform);

            _gadget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            _gadget.GetComponent<InteractableGadget>().slotRef = this;
        }
    }

    public void ReleaseGadget()
    {

        _gadget = null;

    }
}
