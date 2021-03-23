using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Text pickupTxt;

    bool inrange;

    public virtual void pick_up_action(Transform player) {}

     void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            inrange = true;

            pickupTxt.enabled = true;

            StartCoroutine(whileInRange(col.transform));
        }
    }

    IEnumerator whileInRange(Transform p)
    {
        while(inrange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pickupTxt.enabled = false;
                pick_up_action(p);
                Destroy(gameObject);
            }

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
