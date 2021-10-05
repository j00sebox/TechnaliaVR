using System;
using UnityEngine;

public class InteractableGadget : MonoBehaviour
{
    [SerializeField]
    private float _replaceTime = 3f;

    private EventManager _eventManager;

    [HideInInspector]
    public bool held = false;

    [HideInInspector]
    public VRSlot slotRef;

    private Timer _timer;

    private Rigidbody _rb;

    [Flags]
    public enum GadgetState
    {
        NONE = 0,
        AQUIRED = 1,
        HELD = 2,
        INSLOT = 4,
    };

    public GadgetState CurrentState { get; private set; }

    public VRSlot CurrentSlot { get; private set; }

    void Start()
    {
        _eventManager = EventManager.Instance;

        _rb = GetComponent<Rigidbody>();

        _timer = new Timer(_replaceTime, () => { _eventManager.GadgetReturn(this); });
    }

    void Update()
    {
        if( CurrentState.HasFlag(GadgetState.AQUIRED) && 
            !CurrentState.HasFlag(GadgetState.HELD) && 
            !CurrentState.HasFlag(GadgetState.INSLOT) )
        {
            _timer.Tick(Time.deltaTime);
        }
        else
        {
            _timer.Reset();
        }
    }

    public void OnGrab() {

        if( !CurrentState.HasFlag(GadgetState.AQUIRED) )
        {
            CurrentState |= GadgetState.AQUIRED;
        }
         
        CurrentState |= GadgetState.HELD;

        CurrentState &= ~GadgetState.INSLOT;

        if(CurrentSlot)
        {

            _rb.constraints = RigidbodyConstraints.None;
            
            transform.SetParent(null);
            
            CurrentSlot.ReleaseGadget();

            CurrentSlot = null;

        }
    }

    public void OnDropInSlot(VRSlot slot)
    {
        CurrentState |= GadgetState.INSLOT;

        CurrentState &= ~GadgetState.HELD;

        CurrentSlot = slot;
    }

    public void OnRelease() { 
        CurrentState &= ~GadgetState.HELD;
    }
}
