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


    


}
