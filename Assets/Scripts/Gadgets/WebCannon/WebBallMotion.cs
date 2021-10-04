using UnityEngine;
using TerrainTools;

public class WebBallMotion : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1500.0f;

    private TerrainEditor _editor;

    private Terrain _tOb;

    private float _startTime;

    private Vector3 _initial;

    public RaycastHit Target { get; set; }

    public float Distance { get; set; }

    public ShootWeb Source { get; set; }

    void Start()
    {
        _editor = GetComponent<TerrainEditor>();
    }

    public void Setup(RaycastHit target, float distance)
    {
        _initial = gameObject.transform.position;
        
        _startTime = Time.time;

        Target = target;

        Distance = distance;
    }

    void Update()
    {
        float dist_done = (Time.time - _startTime) * _speed;

        float ratio_left = dist_done / Distance;

        gameObject.transform.position = Vector3.Lerp(_initial, Target.point, ratio_left);

        if (gameObject.transform.position == Target.point)
        {
            SprayWeb();
        }
    }

    private void SprayWeb()
    {
        _tOb = _editor.GetTerrainAtObject(Target.transform.gameObject);

        if(_tOb != null)
        {
            _editor.SetEditValues(_tOb);

            // convert player world coordinates into terrain coordinates
            _editor.GetCoords(Target, out int terX, out int terZ);

            // produce web texture at specified terrain coordinates
            _editor.ModifyTerrain(terX, terZ, TerrainType.WEB);

        }

        Source.RemoveWebBall(transform);
    }
    
}
