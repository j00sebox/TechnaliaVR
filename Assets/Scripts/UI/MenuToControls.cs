using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToControls : MonoBehaviour
{
    private Button _controlsButton;

    private EventManager _eventManger;

    void Start()
    {
        _eventManger = EventManager.Instance;

        _controlsButton = GetComponent<Button> ();
        _controlsButton.onClick.AddListener(ChangeScreen);
    }

    void ChangeScreen()
    {
        _eventManger.ShowControls(true);
    }
}
