using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public Text read;

    public GameObject entry;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    // if player is in range on collider then they have the ability to read
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<ReadPaper>().InRangeOfPaper(true);

            read.enabled = true;

            _eventManager.OnRead += Read;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<ReadPaper>().InRangeOfPaper(false);

            read.enabled = false;

            _eventManager.OnRead -= Read;
        }
    }

    void Read(bool b)
    {
        read.enabled = !b;

        entry.SetActive(b);
    }
    
}
