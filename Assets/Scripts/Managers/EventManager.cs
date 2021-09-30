using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public Action<bool, GameObject> OnRead;

    public Action OnPause;

    public Action OnUnPause;

    public Action<GameObject> OnItemPickup;

    public Action<InteractableGadget> OnGadgetReturn;

    public Action<bool> OnSetBarActive;

    public Action<float> OnUpdateJumpBar;

    public Action<bool> OnShowControls;

    public void ReadPaper(bool b, GameObject playerRef)
    {
        OnRead?.Invoke(b, playerRef);
    }

    public void Pause()
    {
        OnPause?.Invoke();
    }

    public void UnPause()
    {
        OnUnPause?.Invoke();
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

    public void ShowControls(bool b)
    {
        OnShowControls?.Invoke(b);
    }
}
