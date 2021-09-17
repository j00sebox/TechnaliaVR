using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private Text _pickupText;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    protected abstract void PickUpAction(GameObject player);

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            _pickupText.enabled = true;

            _eventManager.OnItemPickup += PickUpAction;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            _pickupText.enabled = false;

            _eventManager.OnItemPickup -= PickUpAction;
        }
    }

    void OnDestroy()
    {

        if(_pickupText != null) _pickupText.enabled = false;
        _eventManager.OnItemPickup -= PickUpAction;
    }
}
