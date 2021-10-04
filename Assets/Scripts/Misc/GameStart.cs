using System.Collections;
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

        StartCoroutine("WaitForClip");

    }

    IEnumerator WaitForClip()
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

    }
}
