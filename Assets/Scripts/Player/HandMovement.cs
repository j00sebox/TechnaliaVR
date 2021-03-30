using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform gun;
    protected Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
        if(PickUp.isHoldingFreezeRay){
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
            animator.SetIKPosition(AvatarIKGoal.RightHand,gun.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand,gun.rotation);
        }
    }
}
