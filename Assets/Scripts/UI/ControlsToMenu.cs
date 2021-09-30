using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsToMenu : MonoBehaviour
{
    private Button _backbutton;

    private EventManager _eventManger;

    void Start()
    {
        _eventManger = EventManager.Instance;

        _backbutton = GetComponent<Button> ();
        _backbutton.onClick.AddListener(ChangeScreen);
    }

    void ChangeScreen()
    {
        _eventManger.ShowControls(false);
    }
}
