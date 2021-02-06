using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIce : MonoBehaviour
{
    RaycastHit hit;

    int layerMask;

    public Transform iceSheet;

    ParticleSystem ice;

    ParticleSystem.EmissionModule emission;

    TerrainEditor editor;

    Terrain tob;
    

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        ice = GetComponentInChildren<ParticleSystem> ();
        emission = ice.emission;
        editor = GetComponent<TerrainEditor> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, layerMask))
        {
            if(Input.GetButton("Fire1"))
            {
            
              emission.enabled = true;
              tob = editor.GetTerrainAtObject(hit.transform.gameObject);
              editor.SetEditValues(tob);
              editor.GetCoords(hit, out int terX, out int terZ);
              editor.ModifyTerrain(terX, terZ);
            }
            else
            {
                emission.enabled = false;
            }
        }
        else
        {
            emission.enabled = false;
        }
        
    }
}
