using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : MonoBehaviour
{
    Transform current_gadget;
    static public bool[] gadgets_obtained;
    public Transform[] gadget_prefab;
    public Transform player;
    static public int currentSelection = 0;
    
    void Awake()
    {
        gadgets_obtained = new bool[2] { false, false };
    }

    // Update is called once per frame
    void Update()
    {
        // select gadget to use
        // if(Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     if(currentSelection != 0)
        //     {
        //         currentSelection = 0;
        //         if(gadgets_obtained[currentSelection])
        //         {
        //             SpawnFR();
        //         }
        //     }
        
        // }
        // else if(Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     if(currentSelection != 1)
        //     {
        //         currentSelection = 1;
        //         if(gadgets_obtained[currentSelection])
        //         {   
        //             SpawnWC();
        //         }
        //     }
        // }
    }

    public void SpawnFR()
    {
        if(current_gadget != null)
            Destroy(current_gadget.gameObject);

        current_gadget = Instantiate(gadget_prefab[0], player.position, gadget_prefab[0].rotation);
        current_gadget.parent = player;
        current_gadget.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SpawnWC()
    {
        if(current_gadget != null)
            Destroy(current_gadget.gameObject);

        current_gadget = Instantiate(gadget_prefab[1], player.position, gadget_prefab[1].rotation);
        current_gadget.parent = player;
        current_gadget.rotation = player.rotation * Quaternion.Euler(0, 90, 90);
    }
}
