using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGadget : MonoBehaviour
{
    [SerializeField]
    private float _replaceTime = 3f;

    private float _elapsedTime = 0f;

    public bool held = false;

    public bool inSlot = false;

    private bool _dropped = false;

    public VRSlot slotRef;

    void Update()
    {
        if(!held && !inSlot && !_dropped)
        {
            _dropped = true;

            StartCoroutine("Timer");
        }
    }

    IEnumerator Timer()
    {
        while(_elapsedTime < _replaceTime || _dropped)
        {
            _elapsedTime += Time.deltaTime;

            yield return null;
        }

        _elapsedTime = 0f;

        _dropped = false;
    }

    public void OnGrab() {
         
        held = true; 

        if(slotRef)
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.SetParent(null);
            slotRef.ReleaseGadget();

            slotRef = null;

            inSlot = false;
        }
    }

    public void OnRelease() { 
        held = false; 
    }
}
