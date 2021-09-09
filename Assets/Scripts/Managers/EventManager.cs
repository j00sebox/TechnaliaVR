using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public Action<bool> OnRead;

    public void ReadPaper(bool b)
    {
        OnRead?.Invoke(b);
    }
}
