using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnModifier : MonoBehaviour
{
    [SerializeField]
    private int _index;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    // switch respawn points when player makes it to certain point
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            _eventManager.SetActiveRespawn(_index);
        }
    }
}
