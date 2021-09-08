using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadPaper : MonoBehaviour
{
    public Text read;

    public GameObject entry;

    bool inrange = false;

    // if player is in range on collider then they have the ability to read
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            inrange = true;

            read.enabled = true;

            StartCoroutine("whileInRange");
        }
    }

    // works a lot better than ontriggerstay()
    IEnumerator whileInRange()
    {
        while(inrange)
        {
            // if(Input.GetKeyDown(KeyCode.E))
            // {
            //     if(PauseManager.reading)
            //     {
            //         entry.SetActive(false);
            //         read.enabled = true;
            //         PauseManager.reading = false;
            //     }
            //     else
            //     {
            //         PauseManager.reading = true;
            //         entry.SetActive(true);
            //         read.enabled = false;
            //     }
            // }

            yield return null;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            inrange = false;

            read.enabled = false;
        }
    }
}
