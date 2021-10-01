using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

[RequireComponent(typeof(ActionBasedController))]
public class Hand : MonoBehaviour
{
    private ActionBasedController _controller;

    private float _gripping = 0f;

    private float _sample;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();

        _controller.selectAction.action.started += SetAnim;

        _controller.selectAction.action.canceled += SetIdle;

    }

    // void Update()
    // {
    //     _sample = _controller.selectAction.action.ReadValue<float>();

    //     if(_gripping == 1f && _sample == 0f)
    //     {
    //         SetIdle();
    //     }

    //     _gripping = _sample;
    // }

    void SetAnim(UnityEngine.InputSystem.InputAction.CallbackContext cc)
    {
        Animator anim = _controller.model.GetComponentInChildren<Animator>();

        anim.SetBool("Grip", true);
    }

    void SetIdle(UnityEngine.InputSystem.InputAction.CallbackContext cc)
    {
        Animator anim = _controller.model.GetComponentInChildren<Animator>();

        anim.SetBool("Grip", false);
    }
}
