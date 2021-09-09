using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    public static bool reading = false;
    public static bool paused = false;

    public RectTransform menu;

    public RectTransform controls;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnRead += SetRead;
    }

    void Pause()
    {
        Time.timeScale = 0;

        paused = true;
    }

    void UnPause()
    {
        Time.timeScale = 1;

        paused = false;
    }

    void SetRead(bool read)
    {
        reading = read;
    }


    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     paused = !paused;

        //     if(controls.gameObject.activeSelf)
        //     {
        //         // if player wants to exit while controls screen is up it should also close
        //         controls.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         // the visibility of the menu depends on the value of paused
        //         menu.gameObject.SetActive(paused);
        //     }
            

        //     UpdateCursorState();
        // }
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
