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

    MovingPlatform platform;

    FreezePanel freezePanel;
    

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
        // cast from gun to ground
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            // if player decides to shoot freeze ray some prework must be done to change texture of ground
            if(Input.GetButton("Fire1"))
            {
                // turn particle effect on
                emission.enabled = true;

                if(hit.collider.tag == "MovingPlat")
                {
                    platform = hit.transform.gameObject.GetComponent<MovingPlatform> ();

                    if(!platform.iced)
                    {
                        platform.Icy();
                    }
                }
                else if(hit.collider.tag == "SolarPanel")
                {
                    freezePanel = hit.transform.gameObject.GetComponent<FreezePanel> ();

                    if(!freezePanel.frozen)
                    {
                        freezePanel.PowerPlatforms();
                    }
                }
                else
                {
                    // get reference to terrain object
                    tob = editor.GetTerrainAtObject(hit.transform.gameObject);

                    if(tob != null)
                    {
                        editor.SetEditValues(tob);

                        // convert player world coordinates into terrain coordinates
                        editor.GetCoords(hit, out int terX, out int terZ);

                        // produce ice at specified terrain coordinates
                        editor.ModifyTerrain(terX, terZ);
                    }
                }
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
