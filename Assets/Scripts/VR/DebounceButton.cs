using System.Collections;
using System;
using UnityEngine;
using UnityEngine.XR;

public class DebounceButton
{
    private InputDevice _device;

    private InputFeatureUsage<bool> _button;

    private float _debounceTime;

    private bool _state;

    private bool _prevState;

    private bool _debounce = false;

    private float _timeElapsed;

    private Action _buttonPressCallback;

    private DebounceButton() {}

    public DebounceButton(XRNode nodeToPoll, InputFeatureUsage<bool> feat, Action onPressCallback, float dBounceT = 0.2f)
    {
        _device = InputDevices.GetDeviceAtXRNode(nodeToPoll);

        _button = feat;

        _device.TryGetFeatureValue(_button, out _state);

        _prevState = _state;

        _buttonPressCallback = onPressCallback;

        _debounceTime = dBounceT;
    }

    public void PollButton()
    {
        if(_debounce)
        {
            _timeElapsed += Time.deltaTime;

            if(_timeElapsed >= _debounceTime)
            {
                _debounce = false;

                _timeElapsed = 0f;
            }
        }

        _device.TryGetFeatureValue(_button, out _state);

        if(!_debounce) { 

            if(_state)
                _buttonPressCallback?.Invoke();

           _debounce = true; 
        }

    }
}
