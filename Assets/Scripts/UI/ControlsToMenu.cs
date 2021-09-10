using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsToMenu : MonoBehaviour
{
     Button controls;

    public RectTransform menu;

    public RectTransform control;

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<Button> ();
        controls.onClick.AddListener(ChangeScreen);
    }

    void ChangeScreen()
    {
        menu.gameObject.SetActive(true);

        control.gameObject.SetActive(false);
    }
}
