﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;

    public AudioSource backgroundNoise;

    private AudioSource _planeCrashSound;

    private EventManager _eventManager;

    void Start()
    {
        _eventManager = EventManager.Instance;

        PauseManager.paused = true;

        _planeCrashSound = GetComponent<AudioSource>();

        _planeCrashSound.Play();

        StartCoroutine("GameOpening");

    }

    IEnumerator GameOpening()
    {
        while(_planeCrashSound.isPlaying)
        {
            yield return null;
        }

        anim.SetTrigger("FadeIn");

        PauseManager.paused = false;

        backgroundNoise.Play();

        yield return new WaitForSeconds(3f);

        _eventManager.DisplayTutorial("You can access the menu by pressing the left menu button.");

        yield return new WaitForSeconds(7f);

        _eventManager.DisplayTutorial("The blue orb is holster for your gadgets. You can place gadgets there for safe keeping." +
                                       "You have one on your side and one on your back.");

        _eventManager.DisplayTutorial("If you happen to lose a gadget don't worry it will automatically come back to one of your holsters after a few seconds.");

    }
}
