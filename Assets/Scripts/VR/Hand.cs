using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

[RequireComponent(typeof(ActionBasedController))]
public class Hand : MonoBehaviour
{
    private ActionBasedController _controller;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();

        _controller.selectAction.action.performed += SetAnim;

        _controller.selectAction.action.canceled += SetIdle;
    }

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
