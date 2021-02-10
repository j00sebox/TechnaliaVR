using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    Animator anim;

    public float walkSpeed = 9f;
    public float sprintSpeed = 14f;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;

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
    }

    // Update is called once per frame
    void Update()
    {

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out toFloor, 1, layerMask))
        {

            // change player's body rotation if on sloped terrain
            // the cross product of the floor's normal to the player's current right vector will give us new forward 
            // transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, toFloor.normal));

            // get current terrain object player is standing on, if none returns null
            terrain = tEdit.GetTerrainAtObject(toFloor.transform.gameObject);

            if(terrain != null)
            {
                tEdit.SetEditValues(terrain);

                // get heightmap coords
                tEdit.GetCoords(toFloor, out int terX, out int terZ);

                // could cause problems
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

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(z != 0 || x != 0)
        {
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
        }

        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);
        

        if(Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if(onIce)
        {
            velocity += transform.forward*z;
        }
        else
        {
            // this will gradually slow down the player
            velocity.x *= 90f/100f;
            velocity.z *= 90f/100f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

}


