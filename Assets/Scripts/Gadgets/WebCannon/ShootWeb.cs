using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{
    [SerializeField]
    private Transform _webPrefab;

    private RaycastHit _hit;

    private int _layerMask;

    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }

    public void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 100, _layerMask))
        {
            Transform proj = Instantiate(_webPrefab, gameObject.transform.position, _webPrefab.rotation);
            proj.GetComponent<WebBallMotion>().target = _hit;
            proj.GetComponent<WebBallMotion>().distance = (_hit.point - transform.position).sqrMagnitude;
            proj.forward = gameObject.transform.forward;
        }
    }

}
