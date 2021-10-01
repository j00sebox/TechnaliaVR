using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGadget : MonoBehaviour
{
    [SerializeField]
    private float _replaceTime = 3f;

    private float _elapsedTime = 0f;

    private bool _dropped = false;

    private bool _aquired = false;

    private EventManager _eventManager;

    [HideInInspector]
    public bool held = false;

    [HideInInspector]
    public VRSlot slotRef;

    private Timer _timer;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _timer = new Timer(_replaceTime, () => { _eventManager.GadgetReturn(this); });
    }

    void Update()
    {
        if( _aquired && ( !held && slotRef == null ) )
        {
            _dropped = true;

            _timer.Tick(Time.deltaTime);
        }
        else
        {
            _timer.Reset();
        }
    }

    public void OnGrab() {

        if(!_aquired)
        {
            _aquired = true;
        }
         
        held = true; 

        if(slotRef)
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.SetParent(null);
            slotRef.ReleaseGadget();

            slotRef = null;
        }
    }

    public void OnRelease() { 
        held = false; 
    }
}
