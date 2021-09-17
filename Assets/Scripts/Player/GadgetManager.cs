using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : Singleton<GadgetManager>
{
    private EventManager _eventManager;

    private List<VRSlot> _availableSlots;

    void Start()
    {
        _availableSlots = new List<VRSlot>(GameObject.FindObjectsOfType<VRSlot>());

        _eventManager = EventManager.Instance;

        _eventManager.OnGadgetReturn += Relocate;
    }

    private void Relocate(InteractableGadget ig) 
    { 
        VRSlot slot = GetAvailableSlot();

        if(slot)
            ig.gameObject.transform.position = slot.gameObject.transform.position;
    }

    private VRSlot GetAvailableSlot()
    {
        return _availableSlots.Find(x => x.Gadget == null);
    }

    
}
