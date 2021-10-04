using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public bool forwards = true;

    Vector3 origin;

    public bool xdir = true;

    public int range = 200;

    public float movementSpeed = 1.5f;

    private RespawnManager _rm;

    void Start()
    {
        // save the original position of the platform
        origin = transform.position;

        _rm = RespawnManager.Instance;
    }

    void FixedUpdate()
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

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<PlayerKill>().Kill();
        }
    }
}
