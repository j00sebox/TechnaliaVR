using System;


public class Timer
{
    public float Duration { 

        get
        {
            return _duration;
        } 

        private set 
        {
            _duration = value;
        } 
    }

    private float _duration;

    private float _elapsedTime = 0f;

    private Action OnComplete;

    private Timer() {}

    public Timer(float duration, Action actionToPerform) 
    {
        Duration = duration;

        OnComplete = actionToPerform;
    }

    public void Tick(float deltaTime)
    {
        if(_elapsedTime < _duration)
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
