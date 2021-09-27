using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ReadPaper : MonoBehaviour
{
    [SerializeField]
    private XRNode _leftInput;

    private DebounceButton _db;

    private EventManager _eventManager;

    private PlayerMovement _pMovement;

    private bool _readState = false;

    public bool inRange = false;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _pMovement  = GetComponent<PlayerMovement>();

        _db = new DebounceButton(_leftInput, CommonUsages.primaryButton, () => {
            _readState = !_readState;
            _eventManager.ReadPaper(_readState, gameObject);
        });
    }

    void Update()
    {
        if(inRange && _pMovement.IsGrounded())
        {
            _db.PollButton();
        }
    }

}
