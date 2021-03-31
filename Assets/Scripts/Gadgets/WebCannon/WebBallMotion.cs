using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBallMotion : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward*1.1f; 
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
