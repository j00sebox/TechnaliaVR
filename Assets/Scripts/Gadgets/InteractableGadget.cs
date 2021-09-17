using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGadget : MonoBehaviour
{
    [SerializeField]
    private float _replaceTime = 3f;

    private float _elapsedTime = 0f;

    public bool held = false;

    private bool _dropped = false;

    private bool _aquired = false;

    private EventManager _eventManager;

    public VRSlot slotRef;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    void Update()
    {
        if( _aquired && ( !held && slotRef == null && !_dropped ) )
        {
            _dropped = true;

            StartCoroutine("Timer");
        }
    }

    IEnumerator Timer()
    {
        while(_elapsedTime < _replaceTime && _dropped)
        {
            _elapsedTime += Time.deltaTime;

            yield return null;
        }

        _elapsedTime = 0f;

        _eventManager.GadgetReturn(this);

        _dropped = false;
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
