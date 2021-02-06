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
    public float jumpHeight = 3f;

    float speed;

    Vector3 velocity;

    Vector3 interpolatedPos;

    bool onIce = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
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
            velocity.x += x*50f;
            velocity.z += z*50f;
        }
        else
        {
            velocity = Vector3.Lerp(velocity, new Vector3(0, velocity.y, 0), 45f);
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Col)
    {
        if(Col.tag == "Ice")
        {
            onIce = true;
        }
    }

    void OnTriggerExit()
    {
        onIce = false;
    }
}


