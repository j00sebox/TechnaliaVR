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

    private bool _inRange = false;

    private bool _readState = false;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _db = new DebounceButton(_leftInput, CommonUsages.primaryButton, () => {
            _readState = !_readState;
            _eventManager.ReadPaper(_readState);
        });
    }

    void Update()
    {
        _db.PollButton();
    }

    public void InRangeOfPaper(bool b)
    {
        _inRange = b;
    }
}
