using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    private RespawnManager _rm;

    void Start()
    {
        _rm = RespawnManager.Instance;
    }

    public void Kill()
    {
        transform.position = _rm.ActiveRespawn.position;
    }
}
