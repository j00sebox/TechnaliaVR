using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : Singleton<RespawnManager>
{
    [SerializeField]
    private Transform[] _respawnPoints;

    private EventManager _eventManager;

    public Transform ActiveRespawn 
    { 
        get
        {
            return _activeRespawn;
        } 
    }

    private Transform _activeRespawn;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnSetActiveRespawn += SetActiveSpawn;

        _activeRespawn = _respawnPoints[0];
    }

    private void SetActiveSpawn(int index)
    {
        _activeRespawn = _respawnPoints[index];
    }

}
