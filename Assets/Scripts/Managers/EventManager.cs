using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public Action<bool, GameObject> OnRead;

    public Action<GameObject> OnItemPickup;

    public Action<InteractableGadget> OnGadgetReturn;

    public Action<bool> OnSetBarActive;

    public Action<float> OnUpdateJumpBar;

    public void ReadPaper(bool b, GameObject playerRef)
    {
        OnRead?.Invoke(b, playerRef);
    }

    public void PickupItem(GameObject go)
    {
        OnItemPickup?.Invoke(go);
    }

    public void GadgetReturn(InteractableGadget ig)
    {
        OnGadgetReturn?.Invoke(ig);
    }

    public void SetBarActive(bool b)
    {
        OnSetBarActive?.Invoke(b);
    }

    public void UpdateJumpBar(float progress)
    {
        OnUpdateJumpBar?.Invoke(progress);
    }
}
