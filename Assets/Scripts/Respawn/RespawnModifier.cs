using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnModifier : MonoBehaviour
{

    public RespawnManager rm;

    public int index;

    // switch respawn points when player makes it to certain point
   void OnTriggerEnter(Collider col)
   {
       if(col.tag == "Player")
       {
           rm.activeRespawn = rm.respawnPoints[index];
       }
   }
}
