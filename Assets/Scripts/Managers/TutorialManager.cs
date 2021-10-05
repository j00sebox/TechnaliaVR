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
    private Dropdown _tutorialDropDown;

    [SerializeField]
    private Text _menuTextArea;

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

    private Queue<(string, string)> _tutorialsInQ;

    private Dictionary<string, string> _foundTuts;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnDisplayTutorial += DisplayTip;

        _tutorialsInQ = new Queue<(string, string)>();

        _foundTuts = new Dictionary<string, string>();

        _timer = new Timer(_displayTime, () => {
            _box.SetActive(false);
        });

        _tutorialDropDown.onValueChanged.AddListener( delegate {
            DropdownItemChanged();
        }); 
    }

    public void ToggleEnable() { _enabled = !_enabled; }

    private void DisplayTip(string title, string text)
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
                _tutorialsInQ.Enqueue((title, text));
            }
        }

        if(!_foundTuts.ContainsKey(title))
        {
            _foundTuts.Add(title, text);
                
            Dropdown.OptionData dod = new Dropdown.OptionData();
            dod.text = title;

            _tutorialDropDown.options.Add(dod);

            if(_tutorialDropDown.options.Count == 1)
            {
                _tutorialDropDown.captionText.text = _tutorialDropDown.options[0].text;

                _menuTextArea.text = _foundTuts[_tutorialDropDown.options[0].text];
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
            (string, string) tut = _tutorialsInQ.Dequeue();

            DisplayTip(tut.Item1, tut.Item2);
        }
    }

    private void DropdownItemChanged()
    {
        _menuTextArea.text = _foundTuts[_tutorialDropDown.captionText.text];
    }
}
