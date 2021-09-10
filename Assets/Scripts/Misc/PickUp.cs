using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Text pickupTxt;

    bool inrange;

    public static bool isHoldingFreezeRay = false;

    public virtual void pick_up_action() {}

     void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            inrange = true;

            pickupTxt.enabled = true;

            StartCoroutine(whileInRange());
        }
    }

    IEnumerator whileInRange()
    {
        while(inrange)
        {
            // if(Input.GetKeyDown(KeyCode.E))
            // {
            //     pickupTxt.enabled = false;
            //     pick_up_action();
            //     Destroy(gameObject);
            //     isHoldingFreezeRay = true;
            // }

            yield return null;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            inrange = false;

            pickupTxt.enabled = false;
        }
    }
}
