using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadPaper : MonoBehaviour
{
    public Text read;

    public GameObject entry;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            read.enabled = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(PauseManager.reading)
                {
                    entry.SetActive(false);
                    read.enabled = true;
                    PauseManager.reading = false;
                }
                else
                {
                    PauseManager.reading = true;
                    entry.SetActive(true);
                    read.enabled = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            read.enabled = false;
        }
    }
}
