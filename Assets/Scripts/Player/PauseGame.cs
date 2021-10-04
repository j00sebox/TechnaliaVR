using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private XRNode _leftInput;

    [SerializeField]
    private XRNode _rightInput;

    private InputDevice _leftController;

    private InputDevice _rightController;

    private EventManager _eventManager;

    private DebounceButton _db;
    

    void Start()
    {
        _eventManager = EventManager.Instance;

        _leftController = InputDevices.GetDeviceAtXRNode(_leftInput);
        _rightController = InputDevices.GetDeviceAtXRNode(_rightInput);

        _db = new DebounceButton(_leftInput, CommonUsages.menuButton, () => {

            if(!PauseManager.paused)
            {
                _eventManager.Pause();
            }
            else
            {
                _eventManager.UnPause();
            }
        });
    }

    void Update()
    {
        _db.PollButton(Time.unscaledDeltaTime);
    }
}
