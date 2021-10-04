using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{
    [SerializeField]
    private Transform _webPrefab;

    [SerializeField]
    private float _range = 100f;

    [SerializeField, Tooltip("Specifies the number of objects that will be instatiated for the object pool")]
    private int _numberInPool = 25;

    private Queue<Transform> _pool;

    private RaycastHit _hit;

    private int _layerMask;

    void Start()
    {
        _pool = new Queue<Transform>();

        for(int i = 0; i < _numberInPool; i++)
        {
            Transform webBall = Instantiate(_webPrefab, gameObject.transform.position, _webPrefab.rotation);
            webBall.GetComponent<WebBallMotion>().Source = this;
            webBall.gameObject.SetActive(false);

            _pool.Enqueue(webBall);
        }

        _layerMask = LayerMask.GetMask("Ground");
    }

    public void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, _range, _layerMask))
        {
            Transform proj = _pool.Dequeue();

            proj.gameObject.SetActive(true);

            proj.position = gameObject.transform.position;

            proj.GetComponent<WebBallMotion>().Setup(_hit, (_hit.point - transform.position).sqrMagnitude);
            
            proj.forward = gameObject.transform.forward;
        }
    }

    public void RemoveWebBall(Transform ball)
    {
        ball.gameObject.SetActive(false);

        _pool.Enqueue(ball);
    }

}
