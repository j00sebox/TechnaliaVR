using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    Animator anim;

    AudioSource[] footstepSound;

    int sandSound = 1;

    int iceSound = 0;

    public float walkSpeed = 9f;
    public float sprintSpeed = 14f;
    public float gravity = -9.81f;
    public float jumpHeight = 7f;

    public bool springBoots = false;

    float speed;

    Vector3 velocity;

    Vector3 interpolatedPos;

    bool onIce = false;

    RaycastHit toFloor;

    int layerMask;

    TerrainEditor tEdit;

    Terrain terrain;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Ground");
        tEdit = GetComponent<TerrainEditor> ();
        footstepSound = GetComponents<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseManager.paused && !PauseManager.reading)
        {
            // if the player is not in the air set their y velocity to 0
            if(controller.isGrounded && velocity.y < 0)
            {
                velocity.y = 0;
            }

            // get inputs from WASD keys
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // raycast from player to the ground to determine what kind of terrain they are on 
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out toFloor, 1, layerMask))
            {
                // get current terrain object player is standing on, if none returns null
                terrain = tEdit.GetTerrainAtObject(toFloor.transform.gameObject);

                if(terrain != null)
                {
                    tEdit.SetEditValues(terrain);

                    // get heightmap coords
                    tEdit.GetCoords(toFloor, out int terX, out int terZ);

                    if(tEdit.CheckIce(terX, terZ))
                    {
                        onIce = true;
                    }
                    else
                    {
                        onIce = false;
                    }
                }
            }
            else
            {
                onIce = false;
            }

            

            // if any of these keys are pressed appropriate animations should play
            if(z != 0 || x != 0)
            {
                // play different sound effects if the player is currently moving on sand vs ice
                if(onIce)
                {
                    if(!footstepSound[iceSound].isPlaying)
                    {
                        footstepSound[sandSound].Stop();
                        footstepSound[iceSound].Play();
                    }
                }
                else
                {   
                    if(!footstepSound[sandSound].isPlaying)
                    {
                        footstepSound[iceSound].Stop();
                        footstepSound[sandSound].Play();
                    }
                }

                // sprint button changes player's speed
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsRunning", true);
                    speed = sprintSpeed;
                }
                else
                {
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsWalking", true);
                    speed = walkSpeed;
                }
            }
            else
            {
                anim.SetBool("IsWalking", false);

                if(footstepSound[iceSound].isPlaying)
                {
                    footstepSound[iceSound].Stop();
                }

                if(footstepSound[sandSound].isPlaying)
                {
                    footstepSound[sandSound].Stop();
                }
            }

            // move forwards/backwards with z and left/right with x
            Vector3 move = transform.right * x + transform.forward * z;
            
            // character controller handles the movement
            controller.Move(move * speed * Time.deltaTime);
            
            // play jump animation and calculate upwards velocity
            if(Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // gradually brings play back to ground
            velocity.y += gravity * Time.deltaTime;

            // if player is on ice then they gradually gain speed
            if(onIce)
            {
                if(x != 0)
                {
                    velocity += transform.right*0.7f*x;
                }

                if(z != 0)
                {
                    velocity += transform.forward*0.7f*z;
                }
            }
            else
            {
                // this will gradually slow down the player
                velocity.x *= 90f/100f;
                velocity.z *= 90f/100f;
            }

            // apply the velocity to the player
            controller.Move(velocity * Time.deltaTime);
        }
    }
}



