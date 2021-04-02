using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{

    public RespawnManager rm;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.transform.position = rm.activeRespawn.position;
        }
    }
}
