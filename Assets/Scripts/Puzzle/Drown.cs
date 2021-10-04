using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    [SerializeField]
    private Animator _fadeAnim;

    private AudioSource _drowningSound;

    private Collider _meshCollider;


    void Start()
    {
        _drowningSound = GetComponent<AudioSource> ();
        _meshCollider = GetComponent<MeshCollider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            _meshCollider.enabled = false;
    
            StartCoroutine(DrownPlayer(col.gameObject));
        }
    }
   

    public IEnumerator DrownPlayer(GameObject player, float timeToDrown = 2f)
    {
        _fadeAnim.SetTrigger("FadeOut");

        _drowningSound.Play();

        yield return new WaitForSeconds(timeToDrown);

        player.GetComponent<PlayerKill>().Kill();

        _meshCollider.enabled = true;

    }
}
