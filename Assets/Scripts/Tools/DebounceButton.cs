using System.Collections;
using System;
using UnityEngine;
using UnityEngine.XR;

public class DebounceButton
{
    private InputDevice _device;

    private InputFeatureUsage<bool> _button;

    private bool _state;

    private bool _prevState;

    private bool _debounce = false;

    private float _timeElapsed;

    private Action _buttonPressCallback;

    private Timer _timer;

    private DebounceButton() {}

    public DebounceButton(XRNode nodeToPoll, InputFeatureUsage<bool> feat, Action onPressCallback, float dBounceT = 0.2f)
    {
        _device = InputDevices.GetDeviceAtXRNode(nodeToPoll);

        _button = feat;

        _device.TryGetFeatureValue(_button, out _state);

        _prevState = _state;

        _buttonPressCallback = onPressCallback;

        _timer = new Timer(dBounceT, () => { _debounce = false; });
    }

    public void PollButton()
    {
        if(_debounce)
        {
            _timer.Tick(Time.unscaledDeltaTime);
        }

        _device.TryGetFeatureValue(_button, out _state);

        if(!_debounce && _prevState != _state) { 

            if(_state)
                _buttonPressCallback?.Invoke();

           _debounce = true; 
        }

    }
}
