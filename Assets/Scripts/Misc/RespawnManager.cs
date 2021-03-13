using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
   public Transform[] respawnPoints;

   public Transform activeRespawn;

   void Awake()
   {
       activeRespawn = respawnPoints[0];
   }


    // switch respawn points when player makes it to certain point
   void OnTriggerEnter(Collider col)
   {
       if(col.tag == "Player")
       {
           activeRespawn = respawnPoints[1];
       }
   }


}
