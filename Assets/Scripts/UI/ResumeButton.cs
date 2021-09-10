﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    Button resumeButton;

    public RectTransform menu;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton = GetComponent<Button> ();
        resumeButton.onClick.AddListener(ResumeGame);
    }

    void ResumeGame()
    {
        PauseManager.paused = false;

        menu.gameObject.SetActive(false);

        PauseManager.UpdateCursorState();
    }
}