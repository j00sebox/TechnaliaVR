using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{

    public RespawnManager rm;

    Collider player;

    float currCountdownValue;

    public Animator anim;

    AudioSource drowning;

    Collider meshCollider;


    void Start()
    {
        drowning = GetComponent<AudioSource> ();
        meshCollider = GetComponent<MeshCollider>();
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            meshCollider.enabled = false;
            player = col;
            StartCoroutine(StartCountdown());
            drowning.Play();
        }
    }
   

    public IEnumerator StartCountdown(float countdownValue = 2)
    {
        //anim.SetTrigger("FadeOut");
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            // Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        player.transform.position = rm.activeRespawn.position;
         meshCollider.enabled = true;

    }
}
