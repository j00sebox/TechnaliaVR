using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePanel : MonoBehaviour
{

    public MovingPlatform[] platforms;

    public Transform iceSheet;

    public bool frozen = false;

    public void PowerPlatforms()
    {
        frozen = true;
        Instantiate(iceSheet, transform.position, transform.rotation);
        foreach(MovingPlatform p in platforms)
        {
            p.powered = true;
        }
    }
}
