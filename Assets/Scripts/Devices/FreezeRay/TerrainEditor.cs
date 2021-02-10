using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainEditor : MonoBehaviour
{

    Terrain targetT;
    float[,] heightMap;
    int terrainHeightMapWidth;
    int terrainHeightMapHeight;
    TerrainData terrainData;
    float[,,] splat;
    int sampleWidth = 10;
    int sampleHeight = 10;

    public Terrain GetTerrainAtObject(GameObject gameObject)
    {
        if(gameObject.GetComponent<Terrain> ())
        {
            return gameObject.GetComponent<Terrain> ();
        }

        return null;
    }

    public TerrainData GetTerrainData()
    {
        if(targetT)
        {
            return targetT.terrainData;
        }

        return default(TerrainData);
    }

    public Terrain GetTerrain()
    {
        if (targetT)
        {
            return targetT;
        }

        return default(Terrain);
    }

    public void SetEditValues(Terrain t)
    {
        targetT = t;
        terrainData = GetTerrainData();
        heightMap = GetHeightMap();
        terrainHeightMapHeight = GetTerrainHeight();
        terrainHeightMapWidth = GetTerrainWidth();
    }

    public float[,] GetHeightMap()
    {
        if(targetT)
        {
            return targetT.terrainData.GetHeights(0, 0, targetT.terrainData.heightmapResolution, targetT.terrainData.heightmapResolution);
        }

        return default(float[,]);
    }

    public int GetTerrainWidth()
    {
        if (targetT)
        {
            return targetT.terrainData.heightmapResolution;
        }
        return 0;
    }
    public int GetTerrainHeight()
    {
        if (targetT)
        {
            return targetT.terrainData.heightmapResolution;
        }
        return 0;
    }

    public Vector3 GetTerrainSize()
    {
        if (targetT)
        {
            return targetT.terrainData.size;
        }
        return Vector3.zero;
    }

    public void GetCoords(RaycastHit hit, out int x, out int z)
    {
        x = (int)( ( ( hit.point.x - targetT.GetPosition().x ) / targetT.terrainData.size.x ) * targetT.terrainData.heightmapResolution );
        z = (int)( ( ( hit.point.z - targetT.GetPosition().z ) / targetT.terrainData.size.z ) * targetT.terrainData.heightmapResolution );
    }

    public void ModifyTerrain(int x, int z)
    {
        splat = targetT.terrainData.GetAlphamaps(x, z, sampleWidth, sampleHeight);

        for(int k = 0; k < sampleWidth; k++)
        {
            for(int j = 0; j < sampleHeight; j++)
            {
                for(int i = 0; i <= 2; i++)
                {
                    if(i == 2)
                    {
                        splat[k, j, i] = 1;
                    }
                    else
                    {
                        splat[k, j, i] = 0;
                    }
                    
                }
            }
        }

        targetT.terrainData.SetAlphamaps(x, z, splat);
    }

    public bool CheckIce(int x, int z)
    {
        splat = targetT.terrainData.GetAlphamaps(x, z, 1, 1);

        if(splat[0, 0, 2] == 1)
        {
            return true;
        }

        return false;
    }
    
}
