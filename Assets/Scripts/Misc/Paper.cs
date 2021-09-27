using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public Text read;

    public GameObject entry;

    private EventManager _eventManager;

    [SerializeField]
    private Canvas _canvas;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    // if player is in range on collider then they have the ability to read
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            read.enabled = true;

            col.GetComponent<ReadPaper>().inRange = true;

            _eventManager.OnRead += Read;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            read.enabled = false;

            col.GetComponent<ReadPaper>().inRange = false;

            _eventManager.OnRead -= Read;
        }
    }

    void Read(bool b, GameObject playerRef)
    {
        read.enabled = !b;

        entry.SetActive(b);

        if(b)
        {
            AdjustAngle(playerRef);
        }
    }

    // turns the diary entry towards teh player
    private void AdjustAngle(GameObject playerRef)
    {
        float angle = Vector3.Angle(playerRef.transform.forward, _canvas.transform.forward);

        _canvas.transform.LookAt(playerRef.transform);
        
        Vector3 rot = _canvas.transform.rotation.eulerAngles;
        
        // needed to flip text around
        _canvas.transform.rotation = Quaternion.Euler(0, rot.y+180, 0);
    }
    
}
