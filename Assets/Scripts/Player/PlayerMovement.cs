using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private XRNode _leftInput;
    
    [SerializeField]
    private float _walkSpeed = 5f;

    [SerializeField]
    private float _maxSpeed = 20f;

    [SerializeField]
    private float _iceAcceleration = 5.5f;

    [SerializeField]
    private float _gravity = -9.81f;

    private float _fallingSpeed = 0f;

    private float _moveSpeed;

    private CharacterController _cc;

    private RaycastHit _terrainHitInfo;

    private Vector2 _direction;

    private XRRig _rig;

    AudioSource[] footstepSound;

    int sandSound = 1;

    int iceSound = 0;

    public float walkSpeed = 9f;
    public float sprintSpeed = 14f;
    public static float gravity = -9.81f;
    public float jumpHeight = 7f;

    float jumpMod;

    bool charging = false;

    // this makes sure only one Move call updates the collision with the terrains
    bool first_col = false;

    public bool springBoots = false;

    float speed;

    public static Vector3 velocity;

    Vector3 interpolatedPos;

    Vector3 terrainNormal;

    Vector3 slope;

    Vector3 dir;

    Vector3 dir_right;

    bool onIce = false;
    public bool webbed = false;

    RaycastHit toFloor;

    int layerMask;

    TerrainEditor tEdit;

    Terrain terrain;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _rig = GetComponent<XRRig>();
        layerMask = LayerMask.GetMask("Ground");
        tEdit = GetComponent<TerrainEditor> ();

        _moveSpeed = _walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseManager.paused && !PauseManager.reading)
        {

            InputDevice device = InputDevices.GetDeviceAtXRNode(_leftInput);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _direction);

            

            // if the player is moving on the ground there should be footstep sounds
            // if( (z != 0 || x != 0) && _cc.isGrounded)
            // {
            //     // play different sound effects if the player is currently moving on sand vs ice
            //     //if(onIce)
            //     // {
            //     //     if(!footstepSound[iceSound].isPlaying)
            //     //     {
            //     //         footstepSound[sandSound].Stop();
            //     //         footstepSound[iceSound].Play();
            //     //     }
            //     // }
            //     // else
            //     // {   
            //     //     if(!footstepSound[sandSound].isPlaying)
            //     //     {
            //     //         footstepSound[iceSound].Stop();
            //     //         footstepSound[sandSound].Play();
            //     //     }
            //     // }

            //     // sprint button changes player's speed
            //     // if(Input.GetKey(KeyCode.LeftShift))
            //     // {
            //     //     anim.SetBool("IsWalking", false);
            //     //     anim.SetBool("IsRunning", true);
            //     //     speed = sprintSpeed;
            //     // }
            //     // else
            //     // {
            //     //     anim.SetBool("IsRunning", false);
            //     //     anim.SetBool("IsWalking", true);
            //     //     speed = walkSpeed;
            //     // }
            // }
            // else
            // {
            //     anim.SetBool("IsWalking", false);

            //     if(footstepSound[iceSound].isPlaying)
            //     {
            //         footstepSound[iceSound].Stop();
            //     }

            //     if(footstepSound[sandSound].isPlaying)
            //     {
            //         footstepSound[sandSound].Stop();
            //     }
            // }

            // if(springBoots)
            // {
            //     if (Input.GetButton("Jump") && !charging)
            //         StartCoroutine(ChargeJump());   
            // }           
            // else
            // {
            //     // play jump animation and calculate upwards velocity
            //     if(Input.GetButtonDown("Jump"))
            //     {
            //         velocity.y = Mathf.Sqrt( (jumpHeight) * -2f * gravity);
            //     }
            // }

            /**********************MOVEMENT**********************/

            // move forwards/backwards with z and left/right with x
            //Vector3 move = transform.right * x + transform.forward * z;
            
            // character controller handles the movement
            // if(!webbed)
            // {
            //     first_col = true;
            //     _cc.Move(move * speed * Time.deltaTime);
            //     _cc.Move(dir * speed * Time.deltaTime);
            // }

            // // gradually brings play back to ground
            // velocity.y += gravity * Time.deltaTime;

            // if(webbed)
            // {
            //     velocity = new Vector3(0,0,0);
            // }

            // // if player is on ice then they gradually gain speed
            // if(onIce)
            // {
            //     if(x != 0)
            //     {
            //         velocity += transform.right*0.5f*x;
            //     }
            //     else
            //     {
            //         velocity.x *= 99f/100f;
            //     }

            //     if(z != 0)
            //     {
            //         velocity += transform.forward*0.5f*z;
            //     }
            //     else
            //     {
            //         velocity.z *= 99f/100f;
            //     }
            // }
            // else if(!_cc.isGrounded)
            // {
            //     velocity.x *= 0.97f;
            //     velocity.z *= 0.97f;
            // }
            // else
            // {
            //     // this will gradually slow down the player
            //     velocity.x = 0f;
            //     velocity.z = 0f;
            // }

            
            
        }
    }

    void FixedUpdate()
    {
         if(!IsGrounded())
        {
            _fallingSpeed += _gravity * Time.fixedDeltaTime;

            _cc.Move(Vector3.up * _fallingSpeed * Time.fixedDeltaTime);
        }
        else
        {
            CheckTerrain();
            _fallingSpeed = 0f;
        }

        if(onIce)
        {
            _moveSpeed += _iceAcceleration * Time.fixedDeltaTime;
        }
        else if(_moveSpeed > _walkSpeed)
        {
            _moveSpeed -= _iceAcceleration * Time.fixedDeltaTime;
        }

        _moveSpeed = Mathf.Clamp(_moveSpeed, _walkSpeed, _maxSpeed);

        Quaternion headYaw = Quaternion.Euler(0, _rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 v3Dir = headYaw * new Vector3(_direction.x, 0, _direction.y);

        // apply the velocity to the player
        _cc.Move(v3Dir * Time.fixedDeltaTime * _moveSpeed);

       
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.TransformPoint(_cc.center);

        return Physics.SphereCast(origin, _cc.radius, Vector3.down, out _terrainHitInfo, _cc.center.y + 0.01f, layerMask);
    }

    private void CheckTerrain()
    {
        if(_terrainHitInfo.collider.tag == "Ground")
        {
            terrain = tEdit.GetTerrainAtObject(_terrainHitInfo.collider.gameObject);

            dir = Vector3.zero;
            onIce = false;
            webbed = false;

            if(terrain != null)
            {
                tEdit.SetEditValues(terrain);

                // get heightmap coords
                tEdit.GetCoords(_terrainHitInfo.point, out int terX, out int terZ);

                terrainNormal = terrain.terrainData.GetInterpolatedNormal(  (_terrainHitInfo.point.x - terrain.GetPosition().x) / terrain.terrainData.size.x,  (_terrainHitInfo.point.z - terrain.GetPosition().z) / terrain.terrainData.size.z);

                float angle = Vector3.Angle(terrainNormal, Vector3.up);

                if(angle > 45)
                {
                    dir_right = Vector3.Cross(terrainNormal, Vector3.up);
                    dir = Vector3.Cross(terrainNormal, dir_right);
                }

                if(tEdit.CheckIce(terX, terZ))
                {
                    onIce = true;
                }

                if(tEdit.CheckWebbed(terX, terZ))
                {
                    webbed = true;
                }
            }

            first_col = false;
        }
        else
        {
            onIce = false;
        }
    }

    // compress the spring boots to get a larger jump
    IEnumerator ChargeJump()
    {
        charging = true;
        // while the player is still holding down the jump button increase the height they will jump
        while(Input.GetButton("Jump"))
        {
            if(jumpMod<45) {
                jumpMod += 24*Time.deltaTime;
            }
            
            yield return null;
        }

        velocity.y = Mathf.Sqrt( (jumpHeight + jumpMod) * -2f * gravity);
        jumpMod = 0f;
        // apply the velocity to the player
        // apply the velocity to the player
        _cc.Move(velocity * Time.deltaTime);
        charging = false;
        webbed = false;
    }

    

//     void OnControllerColliderHit(ControllerColliderHit hit)
//     {

//         Debug.Log(onIce);
//         if(hit.collider.tag == "Ground")
//         {
//             if(first_col)
//             {
//                 terrain = tEdit.GetTerrainAtObject(hit.collider.gameObject);

//                 dir = Vector3.zero;
//                 onIce = false;
//                 webbed = false;

//                 if(terrain != null)
//                 {
//                     tEdit.SetEditValues(terrain);

//                     // get heightmap coords
//                     tEdit.GetCoords(hit.point, out int terX, out int terZ);

//                     terrainNormal = terrain.terrainData.GetInterpolatedNormal(  (hit.point.x - terrain.GetPosition().x) / terrain.terrainData.size.x,  (hit.point.z - terrain.GetPosition().z) / terrain.terrainData.size.z);

//                     float angle = Vector3.Angle(terrainNormal, Vector3.up);

//                     if(angle > 45)
//                     {
//                         dir_right = Vector3.Cross(terrainNormal, Vector3.up);
//                         dir = Vector3.Cross(terrainNormal, dir_right);
//                     }

//                     if(tEdit.CheckIce(terX, terZ))
//                     {
//                         onIce = true;
//                     }

//                     if(tEdit.CheckWebbed(terX, terZ))
//                     {
//                         webbed = true;
//                     }
//                 }

//                 first_col = false;
//             }
//             else
//                 return;
            
//         }
//         else
//         {
//             dir = Vector3.zero;
//             onIce = false;
//             webbed = false;
//         }
//     }
    
}



