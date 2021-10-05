using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class PauseManager : Singleton<PauseManager>
{
    public static bool reading;
    public static bool paused;

    [SerializeField]
    private RectTransform _menusCanvas;

    [SerializeField]
    private RectTransform _menu;

    [SerializeField]
    private RectTransform _controls;

    [SerializeField]
    private RectTransform _tutorials;

    [SerializeField]
    private XRInteractorLineVisual _leftLR;

    [SerializeField]
    private XRInteractorLineVisual _rightLR;

    private EventManager _eventManager;

    private GameObject _player;

    private GameObject _camera;

    void Start()
    {
        reading = false;
        paused = false;

        // if reloading into scene make sure time scale is 1
        Time.timeScale = 1;

        _eventManager = EventManager.Instance;

        _eventManager.OnRead += SetRead;
        _eventManager.OnPause += Pause;
        _eventManager.OnUnPause += UnPause;
        _eventManager.OnShowControls += ShowControls;
        _eventManager.OnShowTutorials += ShowTutorials;

        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.Find("Main Camera");
    }

    private void Pause()
    {
        Time.timeScale = 0;

        _menu.gameObject.SetActive(true);
        
        // sketchy numbers to adjust menu at correct height and distance
        _menusCanvas.transform.position = _player.transform.position + new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z)*4 + Vector3.up*1.25f; 

        _menusCanvas.LookAt(_player.transform);

        Vector3 rot = _menusCanvas.transform.rotation.eulerAngles;

        _menusCanvas.transform.rotation = Quaternion.Euler(0, rot.y+180, 0);

        // hacky but XR isn't disabling the lines when needed
        _leftLR.enabled = true;
        _rightLR.enabled = true;

        paused = true;
    }

    private void ShowControls(bool b)
    {
        _menu.gameObject.SetActive(!b);

        _controls.gameObject.SetActive(b);
    }

    private void ShowTutorials(bool b)
    {
        _menu.gameObject.SetActive(!b);

        _tutorials.gameObject.SetActive(b);
    }

    private void UnPause()
    {
        Time.timeScale = 1;

        if(_menu.gameObject.activeSelf)
        {
            _menu.gameObject.SetActive(false);
        }
        else
        {
            _controls.gameObject.SetActive(false);
            _tutorials.gameObject.SetActive(false);
        }

        // not ideal but best I can do for now
        _leftLR.enabled = false;
        _rightLR.enabled = false;

        paused = false;
    }

    void SetRead(bool read, GameObject playerRef)
    {
        reading = read;   
    }

}
