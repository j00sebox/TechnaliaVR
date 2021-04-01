using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    float jumpMod;

    bool charging = false;

    public bool springBoots = false;

    float speed;

    Vector3 velocity;

    Vector3 interpolatedPos;

    Vector3 terrainNormal;

    Vector3 slope;

    Vector3 dir;

    Vector3 dir_right;

    bool onIce = false;
    bool webbed = false;

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
            if (Physics.Raycast(transform.position, -transform.up, out toFloor, 1f, layerMask))
            {
                // get current terrain object player is standing on, if none returns null
                terrain = tEdit.GetTerrainAtObject(toFloor.transform.gameObject);

                if(terrain != null)
                {
                    tEdit.SetEditValues(terrain);

                    // get heightmap coords
                    tEdit.GetCoords(toFloor, out int terX, out int terZ);

                    terrainNormal = terrain.terrainData.GetInterpolatedNormal(  (toFloor.point.x - terrain.GetPosition().x) / terrain.terrainData.size.x,  (toFloor.point.z - terrain.GetPosition().z) / terrain.terrainData.size.z);

                    float angle = Vector3.Angle(terrainNormal, transform.up);

                    if(angle > 45)
                    {
                        dir_right = Vector3.Cross(terrainNormal, Vector3.up);
                        dir = Vector3.Cross(terrainNormal, dir_right);
                    }
                    else
                    {
                        dir = Vector3.zero;
                    }

                    if(tEdit.CheckIce(terX, terZ))
                    {
                        onIce = true;
                    }
                    else
                    {
                        onIce = false;
                    }

                    if(tEdit.CheckWebbed(terX, terZ))
                    {
                        webbed = true;
                    }
                    else
                    {
                        webbed = false;
                    }
                }
                else
                {
                    dir = Vector3.zero;
                }
            }
            else
            {
                dir = Vector3.zero;
                onIce = false;
            }

            // if the player is moving on the ground there should be footstep sounds
            if( (z != 0 || x != 0) && controller.isGrounded)
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

            if(springBoots)
            {
                if (Input.GetButton("Jump") && !charging)
                    StartCoroutine(ChargeJump());   
            }           
            else
            {
                // play jump animation and calculate upwards velocity
                if(Input.GetButtonDown("Jump"))
                {
                    anim.SetTrigger("Jump");
                    velocity.y = Mathf.Sqrt( (jumpHeight) * -2f * gravity);
                }
            }

            /**********************MOVEMENT**********************/

            // move forwards/backwards with z and left/right with x
            Vector3 move = transform.right * x + transform.forward * z;
            
            // character controller handles the movement
            if(!webbed)
            {
                controller.Move(move * speed * Time.deltaTime);
                controller.Move(dir * speed * Time.deltaTime);
            }

            // gradually brings play back to ground
            velocity.y += gravity * Time.deltaTime;

            if(webbed)
            {
                velocity = new Vector3(0,0,0);
            }

            // if player is on ice then they gradually gain speed
            if(onIce)
            {
                if(x != 0)
                {
                    velocity += transform.right*0.5f*x;
                }
                else
                {
                    velocity.x *= 99f/100f;
                }

                if(z != 0)
                {
                    velocity += transform.forward*0.5f*z;
                }
                else
                {
                    velocity.z *= 99f/100f;
                }
            }
            else if(!controller.isGrounded)
            {
                velocity.x *= 95f/100f;
                velocity.z *= 95f/100f;
            }
            else
            {
                // this will gradually slow down the player
                velocity.x *= 50f/100f;
                velocity.z *= 50f/100f;
            }

            // apply the velocity to the player
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // compress the spring boots to get a larger jump
    IEnumerator ChargeJump()
    {
        charging = true;
        // while the player is still holding down the jump button increase the height they will jump
        while(Input.GetButton("Jump"))
        {
            jumpMod += 12*Time.deltaTime;

            yield return null;
        }

        anim.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt( (jumpHeight + jumpMod) * -2f * gravity);
        jumpMod = 0f;
        // apply the velocity to the player
        controller.Move(velocity * Time.deltaTime);
        charging = false;
        webbed = false;
    }
}



