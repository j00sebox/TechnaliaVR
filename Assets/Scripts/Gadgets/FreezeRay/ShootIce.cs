using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIce : MonoBehaviour
{
    RaycastHit hit;

    int layerMask;

    public Transform iceSheet;

    TerrainEditor editor;

    Terrain tob;

    MovingPlatform platform;

    FreezePanel freezePanel;

    AudioSource iceAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        editor = GetComponent<TerrainEditor> ();
        iceAudio = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        StartCoroutine("Shooting");
    }

    public void StopShooting()
    {
        StopCoroutine("Shooting");
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 15, layerMask))
            {
                // if(!iceAudio.isPlaying)
                // {
                //     iceAudio.Play();
                // }

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
                        editor.ModifyTerrain(terX, terZ, 2);
        
                    }
                }
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
