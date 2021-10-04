using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField]
    private GameObject _box;

    [SerializeField]
    private Text _tutorialText;

    [SerializeField]
    private float _displayTime = 3f;

    public bool TutorialsEnabled 
    { 
        get
        {
            return _enabled;
        }
    }

    private bool _enabled = true;

    private EventManager _eventManager;

    private Timer _timer;

    private Queue<string> _tutorialsInQ;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnDisplayTutorial += DisplayTip;

        _tutorialsInQ = new Queue<string>();

        _timer = new Timer(_displayTime, () => {
            _box.SetActive(false);
        });
    }

    public void Enable() { _enabled = true; }
    public void Disable() { _enabled = false; }

    private void DisplayTip(string text)
    {
        if(_enabled)
        {
            if(!_box.activeSelf && _tutorialsInQ.Count == 0)
            {
                _box.SetActive(true);

                _tutorialText.text = text;

                StartCoroutine("TickTimer");
            }
            else
            {
                _tutorialsInQ.Enqueue(text);
            }
        }
    }

    private IEnumerator TickTimer()
    {
        while(_box.activeSelf)
        {
            _timer.Tick(Time.unscaledDeltaTime);
            yield return null;
        }

        if(_tutorialsInQ.Count > 0)
        {
            DisplayTip(_tutorialsInQ.Dequeue());
        }
    }
}
