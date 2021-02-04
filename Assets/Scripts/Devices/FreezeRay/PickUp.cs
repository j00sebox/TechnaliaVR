using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Transform dest;

    public Transform prefab;

    public Text pickupTxt;

    Transform t;

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            pickupTxt.enabled = true;

            if(Input.GetKeyDown(KeyCode.E))
            {
                pickupTxt.enabled = false;
                t = Instantiate(prefab, dest.position, prefab.rotation);
                t.parent = GameObject.Find("GunHolder").transform;
                t.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            
        }
    }

    void OnTriggerExit()
    {
        pickupTxt.enabled = false;
    }
}
