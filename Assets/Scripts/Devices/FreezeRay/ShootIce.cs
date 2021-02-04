using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIce : MonoBehaviour
{
    RaycastHit hit;

    int layerMask;

    ParticleSystem ice;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        ice = GetComponentInChildren<ParticleSystem> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if(Input.GetButton("Fire1"))
            {
                ice.Play();
            }
            
            if(ice.isPlaying)
            {
                ice.Stop();
            }
        }

        if(ice.isPlaying)
        {
            ice.Stop();
        }
    }
}
