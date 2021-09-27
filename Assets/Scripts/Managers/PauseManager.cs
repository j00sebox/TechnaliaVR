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

    void SetRead(bool read, GameObject playerRef)
    {
        reading = read;   
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
