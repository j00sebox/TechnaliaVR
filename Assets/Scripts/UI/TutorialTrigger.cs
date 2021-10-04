using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField, TextArea]
    private string text;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            _eventManager.DisplayTutorial(text);

            Destroy(gameObject);
        }
    }
}
