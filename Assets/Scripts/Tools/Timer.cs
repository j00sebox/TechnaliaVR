using System;

public class Timer
{
    public float Duration { 

        get
        {
            return _duration;
        } 
    }

    public float ElapsedTime { 
        get
        {
            return _elapsedTime;
        } 
    }

    private float _duration;

    private float _elapsedTime = 0f;

    private Action OnComplete;

    private Timer() {}

    public Timer(float duration, Action actionToPerform) 
    {
        _duration = duration;

        OnComplete = actionToPerform;
    }

    public void Tick(float deltaTime)
    {
        if(deltaTime < 0f) throw new ArgumentException("Delta time cannot be negative !");

        if( (_elapsedTime + deltaTime) < _duration)
        {
            _elapsedTime += deltaTime;
        }
        else
        {
            _elapsedTime = 0f;
            OnComplete?.Invoke();
        }
    }

    public void Reset() { _elapsedTime = 0f; }
}
