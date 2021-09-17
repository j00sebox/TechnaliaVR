using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public Action<bool> OnRead;

    public Action<GameObject> OnItemPickup;

    public Action<InteractableGadget> OnGadgetReturn;

    public void ReadPaper(bool b)
    {
        OnRead?.Invoke(b);
    }

    public void PickupItem(GameObject go)
    {
        OnItemPickup?.Invoke(go);
    }

    public void GadgetReturn(InteractableGadget ig)
    {
        OnGadgetReturn?.Invoke(ig);
    }
}
