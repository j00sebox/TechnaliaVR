using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool reading = false;
    public static bool paused = false;

    public RectTransform menu;

    public RectTransform controls;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if(controls.gameObject.activeSelf)
            {
                // if player wants to exit while controls screen is up it should also close
                controls.gameObject.SetActive(false);
            }
            else
            {
                // the visibility of the menu depends on the value of paused
                menu.gameObject.SetActive(paused);
            }
            

            UpdateCursorState();
        }
    }

    public static void UpdateCursorState()
    {
        if(paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
