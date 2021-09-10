using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBallMotion : MonoBehaviour
{

    TerrainEditor editor;

    Terrain tob;

    public RaycastHit target;

    float start_time;

    float speed = 1500.0f;

    public float distance;

    Vector3 initial;

    void Awake()
    {
        editor = GetComponent<TerrainEditor> ();
        initial = gameObject.transform.position;
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += gameObject.transform.forward*1.1f; 

        float dist_done = (Time.time - start_time) * speed;

        float ratio_left = dist_done / distance;

        gameObject.transform.position = Vector3.Lerp(initial, target.point, ratio_left);

        if (gameObject.transform.position == target.point)
        {
            spray_web();
        }
    }

    void spray_web()
    {
        tob = editor.GetTerrainAtObject(target.transform.gameObject);

        if(tob != null)
        {
            editor.SetEditValues(tob);

            // convert player world coordinates into terrain coordinates
            editor.GetCoords(target, out int terX, out int terZ);

            // produce ice at specified terrain coordinates
            editor.ModifyTerrain(terX, terZ, 3);

        }

        Destroy(gameObject);
    }

    // void OnCollisionEnter(Collision coll)
    // {

    //     if(coll.gameObject.tag == "Ground")
    //     {
    //         // get reference to terrain object
    //         tob = editor.GetTerrainAtObject(target.transform.gameObject);

    //         if(tob != null)
    //         {
    //             editor.SetEditValues(tob);

    //             // convert player world coordinates into terrain coordinates
    //             editor.GetCoords(target, out int terX, out int terZ);

    //             // produce ice at specified terrain coordinates
    //             editor.ModifyTerrain(terX, terZ, 2);

    //         }

    //         Destroy(gameObject);
    //     }

        
    // }
    
}
