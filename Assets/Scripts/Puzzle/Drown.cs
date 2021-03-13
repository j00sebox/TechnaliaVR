using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{

    public Transform respawn;

    void OnTriggerEnter(Collider col)
    {
        
        if(col.tag == "Player")
        {
            col.transform.position = respawn.position;
        }
    }
}
