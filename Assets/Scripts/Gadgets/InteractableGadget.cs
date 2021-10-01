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

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    void Update()
    {
        if( _aquired && ( !held && slotRef == null ) )
        {
            _dropped = true;

            if(_elapsedTime < _replaceTime)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                _eventManager.GadgetReturn(this);
            }
        }
        else
        {
            _elapsedTime = 0f;
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
