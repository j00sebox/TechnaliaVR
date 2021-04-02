using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class RestoreTerrain : MonoBehaviour
{
    TerrainData td1;
    TerrainData td2;

    void Awake()
    {
        Terrain terrain = GetComponent<Terrain> ();

        td1 = terrain.terrainData;

        // everytime the player modifies the world by putting ice down it needs to be reset at the end
        // this is accomplished by saving the terrain data a the start
        File.Copy("./Assets/Resources/Terrain/" + td1.name + ".asset", "./Assets/Resources/TerrainBackups/" + td1.name + "_backup.asset", true);

        string dataName = "TerrainBackups/" + td1.name + "_backup";
        td2 = Resources.Load<TerrainData>(dataName);

        td2.terrainLayers = td1.terrainLayers;
        td2.SetAlphamaps(0, 0, td1.GetAlphamaps(0, 0, td1.alphamapWidth, td1.alphamapHeight));

        if (td2 == null) 
        {
            Debug.LogError("Terrain with that name does not exist");
            return;
        }
    }

    void OnApplicationQuit()
    {
        restoreTerrain();
    }

    // at the end the terrain maps are set to what the backup maps are
    void restoreTerrain()
    {
        // Terrain collider
        td1.SetHeights(0, 0, td2.GetHeights(0, 0, td1.heightmapResolution, td1.heightmapResolution));
        // Textures
        td1.SetAlphamaps(0, 0, td2.GetAlphamaps(0, 0, td1.alphamapWidth, td1.alphamapHeight));
        // Trees
        td1.treeInstances = td2.treeInstances;
        // Grasses
        //td1.SetDetailLayer(0, 0, 0, td2.GetDetailLayer(0, 0, td1.detailWidth, td1.detailHeight, 0));
    }
}
