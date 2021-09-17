using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PickUp : MonoBehaviour
{

    [SerializeField]
    private XRNode _leftInput;

    private DebounceButton _db;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _db = new DebounceButton(_leftInput, CommonUsages.secondaryButton, () => {
            _eventManager.PickupItem(gameObject);
        });
    }

    void Update()
    {
        _db.PollButton();
    }
    
}
