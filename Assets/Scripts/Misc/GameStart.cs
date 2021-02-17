using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;

    public AudioSource backgroundNoise;

    AudioSource planeCrash;

    void Start()
    {
        planeCrash = GetComponent<AudioSource> ();

        planeCrash.Play();

        StartCoroutine("waitForClip");

    }

    IEnumerator waitForClip()
    {
        while(planeCrash.isPlaying)
        {
            yield return null;
        }

        anim.SetTrigger("FadeIn");

        backgroundNoise.Play();
    }
}
