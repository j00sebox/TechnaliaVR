using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class VRSlot : MonoBehaviour
{

    public GameObject Gadget { get; set; }

    [SerializeField]
    private XRRig _rig;

    [SerializeField]
    private float _yPosoffset;

    [SerializeField]
    private float _angleOffset = 90f;

    public void AttachToSlot(GameObject gadget)
    {
        Gadget = gadget;

        Gadget.transform.position = gameObject.transform.position;

        Gadget.transform.SetParent(transform);

        Gadget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Gadget.GetComponent<InteractableGadget>().OnDropInSlot(this);
    }

    void OnTriggerEnter(Collider coll)
    {

        if(Gadget == null && coll.tag == "Gadget" && !coll.gameObject.GetComponent<InteractableGadget>().CurrentState.HasFlag(InteractableGadget.GadgetState.HELD) )
        {
            AttachToSlot(coll.gameObject);
        }
    }

    public void ReleaseGadget()
    {
        Gadget = null;
    }

    void FixedUpdate()
    {
        FollowHeadset();
    }
    
    private void FollowHeadset()
    {
        Vector3 _xzoff = Quaternion.Euler(0, _angleOffset, 0) * (_rig.cameraGameObject.transform.forward* 0.5f);

        Vector3 _posInRig = transform.InverseTransformPoint(_rig.cameraGameObject.transform.position + new Vector3(_xzoff.x, _yPosoffset, _xzoff.z));

        transform.position = transform.TransformPoint(_posInRig);

    }
}
