using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{

    public Transform web_prefab;

    RaycastHit hit;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, layerMask))
            {
                Transform proj = Instantiate(web_prefab, gameObject.transform.position, web_prefab.rotation);
                proj.GetComponent<WebBallMotion>().target = hit;
                proj.GetComponent<WebBallMotion>().distance = (hit.point - transform.position).sqrMagnitude;
                proj.forward = gameObject.transform.forward;
            }
            
        }
    }
}
