using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movementSpeed = 1f;

    public int range = 50;

    public Transform iceSheet;

    public bool iced = false;

    public bool xdir = true;

    float width;

    float length;

    Vector3 origin;

    // does not neccsiarily mean it is going forwards it just ditinguishes it is going in one direction or the opposite
    bool forwards = true;

    void Start()
    {
        // save the original position of the platform
        origin = transform.position;
    }

    void FixedUpdate()
    {
        if(!iced)
        {
            // need to check direction to move it
            if(forwards)
            {
                if(xdir)
                {
                    posxDir();
                }
                else
                {
                    poszDir();
                }
            }
            else
            {
                if(xdir)
                {
                    negxDir();
                }
                else
                {
                    negzDir();
                }
            }
        }
    }

    void posxDir()
    {
        // if the platform hasn't reached the desired location keep moving it
        if(transform.position.x < origin.x + range)
        {
            // increment by the movement speed
            transform.position = new Vector3((transform.position.x + movementSpeed), transform.position.y, transform.position.z);
        }
        // once it has reached the end it has to go to the other extreme
        else 
        {
            forwards = false;
        }
    }

    void negxDir()
    {
        if(transform.position.x > origin.x - range)
        {
            transform.position = new Vector3((transform.position.x - movementSpeed), transform.position.y, transform.position.z);
        }
        else
        {   
            forwards = true;
        }
    }
    
    void poszDir()
    {
        // if the platform hasn't reached the desired location keep moving it
        if(transform.position.z < origin.z + range)
        {
            // increment by the movement speed
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z  + movementSpeed));
        }
        // once it has reached the end it has to go to the other extreme
        else 
        {
            forwards = false;
        }
    }

    void negzDir()
    {
        // if the platform hasn't reached the desired location keep moving it
        if(transform.position.z > origin.z - range)
        {
            // increment by the movement speed
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z  - movementSpeed));
        }
        // once it has reached the end it has to go to the other extreme
        else 
        {
            forwards = true;
        }
    }

    public void Icy()
    {
        iced = true;
        StartCoroutine("IceWait");
    }

    IEnumerator IceWait()
    {
        Transform prefab = Instantiate(iceSheet, new Vector3(transform.position.x + 10f, -0.4f, transform.position.z - 10f), Quaternion.identity);

        yield return new WaitForSeconds(3);

        Destroy(prefab.gameObject);

        iced = false;
    }

    void OnTriggerEnter(Collider col)
    {   
        if(col.tag == "Player")
        {
            col.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            col.transform.parent = null;
        }
    }
        
}
