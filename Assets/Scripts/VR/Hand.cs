using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

[RequireComponent(typeof(ActionBasedController))]
public class Hand : MonoBehaviour
{
    private ActionBasedController _controller;

    private Animator _handAnim;

    private float _sample;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();

        _controller.selectAction.action.started += SetAnim;

        _controller.selectAction.action.canceled += SetIdle;

    }

    void SetAnim(UnityEngine.InputSystem.InputAction.CallbackContext cc)
    {   
        if(_handAnim == null)
            _handAnim = _controller.model.GetComponentInChildren<Animator>();

        _handAnim.SetBool("Grip", true);
    }

    void SetIdle(UnityEngine.InputSystem.InputAction.CallbackContext cc)
    {
        _handAnim.SetBool("Grip", false);
    }

    public void ResetAnim()
    {
        _handAnim.SetBool("Grip", false);
    }
}
