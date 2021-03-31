using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{

    public Transform web_prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Transform proj = Instantiate(web_prefab, gameObject.transform.position, web_prefab.rotation);
            proj.forward = gameObject.transform.forward;
        }
    }
}
