﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIce : MonoBehaviour
{

    [SerializeField]
    private LayerMask _layerMask;

    private RaycastHit _hit;

    private TerrainEditor _editor;

    private Terrain _tob;

    private MovingPlatform _platform;

    private FreezePanel _freezePanel;

    public Transform iceSheet;

    AudioSource iceAudio;


    // Start is called before the first frame update
    void Start()
    {
        _editor = GetComponent<TerrainEditor> ();
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 15, _layerMask))
            {
                // if(!iceAudio.isPlaying)
                // {
                //     iceAudio.Play();
                // }

                if(_hit.collider.tag == "MovingPlat")
                {
                    _platform = _hit.transform.gameObject.GetComponent<MovingPlatform> ();

                    if(!_platform.iced)
                    {
                        _platform.Icy();
                    }
                }
                else if(_hit.collider.tag == "SolarPanel")
                {
                    _freezePanel = _hit.transform.gameObject.GetComponent<FreezePanel> ();

                    if(!_freezePanel.frozen)
                    {
                        _freezePanel.PowerPlatforms();
                    }
                }
                else
                {
                    // get reference to terrain object
                    _tob = _editor.GetTerrainAtObject(_hit.transform.gameObject);

                    if(_tob != null)
                    {
                        _editor.SetEditValues(_tob);

                        // convert player world coordinates into terrain coordinates
                        _editor.GetCoords(_hit, out int terX, out int terZ);

                        // produce ice at specified terrain coordinates
                        _editor.ModifyTerrain(terX, terZ, 2);

                    }
                }
            }

            yield return null;
        }
    }
}
