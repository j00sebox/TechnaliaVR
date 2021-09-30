using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    Button resumeButton;

    public RectTransform menu;

    private EventManager _eventManager;

    // Start is called before the first frame update
    void Start()
    {
        _eventManager = EventManager.Instance;

        resumeButton = GetComponent<Button> ();
        resumeButton.onClick.AddListener(_eventManager.UnPause);
    }

    void ResumeGame()
    {
        PauseManager.paused = false;

        menu.gameObject.SetActive(false);

       
    }
}
