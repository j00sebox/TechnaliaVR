using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TerrainTools;
using System.Runtime.CompilerServices;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private XRNode _leftInput;

    [SerializeField]
    private XRNode _rightInput;
    
    [SerializeField]
    private float _walkSpeed = 5f;

    [SerializeField]
    private float _maxSpeed = 20f;

    [SerializeField]
    private float _iceAcceleration = 5.5f;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _extraHeight = 0.2f;

    [SerializeField]
    private float _jumpVel = 7f;

    [SerializeField]
    private float _maxJumpMod = 45f;

    [SerializeField]
    private float _slidingFactor = 0.5f;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private AudioClip _iceSound;

    [SerializeField]
    private AudioClip _sandSound;

    private bool _canJump;

    private float _timeElapsed;

    private float _yVel;

    private float _fallingSpeed = 0f;

    private float _moveSpeed;

    private bool _charging = false;

    private CharacterController _cc;

    private RaycastHit _terrainHitInfo;

    private Vector2 _direction;

    private XRRig _rig;

    private bool _aButtonState = false;

    private bool _prevState = false;

    private AudioSource _footstepSounds;

    public bool springBoots = false;

    private bool _onIce = false;

    public bool webbed = false;

    private TerrainEditor _tEdit;

    private Terrain _terrain;

    private InputDevice _leftController;

    private InputDevice _rightController;

    private EventManager _eventManager;

    private bool _grounded;

    void Start()
    {
        _eventManager = EventManager.Instance;

        _cc = GetComponent<CharacterController>();

        _rig = GetComponent<XRRig>();

        _tEdit = GetComponent<TerrainEditor> ();

        _footstepSounds = GetComponent<AudioSource>();

        _moveSpeed = _walkSpeed;

        _leftController = InputDevices.GetDeviceAtXRNode(_leftInput);

        _rightController = InputDevices.GetDeviceAtXRNode(_rightInput);
    }

    void Update()
    {
        if(!PauseManager.paused && !PauseManager.reading)
        {
            _leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out _direction);

            _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out _aButtonState);

            if(_aButtonState && !_prevState)
            {
                Jump();
            }
            else if(!_aButtonState && _prevState)
            {
                _charging = false;
            }

            _prevState = _aButtonState;
            
        }
    }

    void FixedUpdate()
    {

        if(!PauseManager.reading)
        {

            // makes sure the capsule collider moves with the headset
            CapsuleFollowHeadset();

            
            /**********GRAVITY**********/

            _grounded = IsGrounded();

            if(_grounded)
            {
                CheckTerrain();

                _canJump = true;
            }
            else
            {  
                _canJump = false;
                webbed = false;
                _onIce = false;

                if(_cc.velocity.y < 0)
                    _fallingSpeed = 0;
                else
                    _fallingSpeed += _gravity * Time.fixedDeltaTime;
            }

            _cc.Move(Vector3.up * _fallingSpeed * Time.fixedDeltaTime);

            /**********ICE**********/

            if(IsMoving())
            {
                if(_onIce)
                {
                    _moveSpeed += _iceAcceleration * Time.fixedDeltaTime;

                    PlayClip(_iceSound);
                }
                else
                {
                    if(_moveSpeed > _walkSpeed)
                    {
                        _moveSpeed -= _iceAcceleration * 2f * Time.fixedDeltaTime;
                    }

                    PlayClip(_sandSound);
                }
                
                _moveSpeed = Mathf.Clamp(_moveSpeed, _walkSpeed, _maxSpeed);
            }
            else
            {
                if(_footstepSounds.isPlaying)
                {
                    _footstepSounds.Stop();
                }

                _moveSpeed = _walkSpeed;
            }

            /**********MOVE**********/

            Quaternion headYaw = Quaternion.Euler(0, _rig.cameraGameObject.transform.eulerAngles.y, 0);

            Vector3 v3Dir = headYaw * new Vector3(_direction.x, 0, _direction.y);

            // apply the velocity to the player
            if(!webbed)
            {
                _cc.Move(v3Dir * _moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private bool IsMoving()
    {
        return _direction.x != 0 || _direction.y != 0;
    }

    private void PlayClip(AudioClip ac)
    {
        if(_grounded && !PauseManager.paused)
        {
            if(ac != _footstepSounds.clip)
            {
                StopSound();
            
                _footstepSounds.clip = ac;
            }

            if(!_footstepSounds.isPlaying)
            {
                _footstepSounds.Play();
            }
        }
        else
        {
            StopSound();
        }
    }

    private void StopSound()
    {
        if(_footstepSounds.isPlaying)
        {
            _footstepSounds.Stop();
        }
    }

    private void Jump()
    {
        if(!_charging && _canJump)
        {
            if(springBoots)
            {
                StartCoroutine(ChargeJump());
            }
            else if(!webbed)
            {
                _fallingSpeed = Mathf.Sqrt( _jumpVel * -2f * _gravity );
            }
                
        }
    }

    public bool IsGrounded()
    {
        Vector3 origin = transform.TransformPoint(_cc.center);

        return Physics.SphereCast(origin, _cc.radius, Vector3.down, out _terrainHitInfo, _cc.center.y + 0.01f, _layerMask);
    }

    private void CheckTerrain()
    {
        if(_terrainHitInfo.collider.tag == "Ground")
        {
            _terrain = _tEdit.GetTerrainAtObject(_terrainHitInfo.collider.gameObject);

            Vector3 slideDir = Vector3.zero;
            Vector3 dirRight;
            Vector3 terrainNormal;

            _onIce = false;
            webbed = false;

            if(_terrain != null)
            {
                _tEdit.SetEditValues(_terrain);

                // get heightmap coords
                _tEdit.GetCoords(_terrainHitInfo.point, out int terX, out int terZ);
                    
                _onIce = _tEdit.CheckIce(terX, terZ);

                webbed = _tEdit.CheckWebbed(terX, terZ);

                if(!webbed)
                {
                    terrainNormal = _terrain.terrainData.GetInterpolatedNormal(  (_terrainHitInfo.point.x - _terrain.GetPosition().x) / _terrain.terrainData.size.x,  (_terrainHitInfo.point.z - _terrain.GetPosition().z) / _terrain.terrainData.size.z);

                    float angle = Vector3.Angle(terrainNormal, Vector3.up);

                    if(angle > 45)
                    {
                        dirRight = Vector3.Cross(terrainNormal, Vector3.up);
                        slideDir = Vector3.Cross(terrainNormal, dirRight);
                    }

                    _cc.Move(slideDir * angle * _slidingFactor * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            _onIce = false;
            webbed = false;
        }
    }

    private void CapsuleFollowHeadset()
    {
        _cc.height = _rig.cameraInRigSpaceHeight + _extraHeight;

        Vector3 center = transform.InverseTransformPoint(_rig.cameraGameObject.transform.position);

        _cc.center = new Vector3(center.x, _cc.height/2 + _cc.skinWidth, center.z);

    }

    // compress the spring boots to get a larger jump
    private IEnumerator ChargeJump()
    {
        float jumpMod = 0f;

        _charging = true;

        _eventManager.SetBarActive(true);

        // while the player is still holding down the jump button increase the height they will jump
        while(_charging)
        {
            if(jumpMod < _maxJumpMod) {
                jumpMod += 24f*Time.deltaTime;
                _eventManager.UpdateJumpBar(jumpMod/_maxJumpMod);
            }
            
            yield return null;
        }

        // half or more of charge will allow player to escape webbing
        if(jumpMod*2 >= _maxJumpMod)
        {
            webbed = false;
        }
        
        if(!webbed)
        {
            _fallingSpeed = Mathf.Sqrt( (_jumpVel + jumpMod) * -2f * _gravity );
        }

        _eventManager.SetBarActive(false);
    }
}



