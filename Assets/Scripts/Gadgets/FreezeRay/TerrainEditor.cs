using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TerrainEditor : MonoBehaviour
{

    Terrain targetT;
    float[,] heightMap;
    int terrainHeightMapWidth;
    int terrainHeightMapHeight;
    TerrainData terrainData;
    TerrainData orig;
    int sampleWidth = 10;
    int sampleHeight = 10;
    float[,,] splat;
    int iceLayer = 2;

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

    // turn the world coordinates into the indicies of the height/aplha map
    public void GetCoords(RaycastHit hit, out int x, out int z)
    {
        x = (int)( ( ( hit.point.x - targetT.GetPosition().x ) / targetT.terrainData.size.x ) * targetT.terrainData.heightmapResolution );
        z = (int)( ( ( hit.point.z - targetT.GetPosition().z ) / targetT.terrainData.size.z ) * targetT.terrainData.heightmapResolution );
    }

    public void ModifyTerrain(int x, int z, int layer)
    {

        int width, height;
        // this keeps track of how many cells in each row of the ice patch shouldn't be changed
        int counter = 2;

        // if this is true the entire sample width can be taken
        if(x + sampleWidth <= 512)
        {
            width = sampleWidth;
        }
        else // else the sample width the difference between the edge of the terrain and the hit coordinate
        {
            width = 512 - x;
        }

        // same thing for sample height
        if(z + sampleHeight <= 512)
        {
            height = sampleHeight;
        }
        else
        {
            height = 512 - z;
        }

        // store these as variables for use later
        int halfw = width/2;
        int halfh = height/2;

        // this makes it so the ice is centered around the hit point
        int newx = x - halfw;

        if (newx < 0)
        {
            newx = x;
        }

        int newz = z - halfh;

        if (newz < 0)
        {
            newz = z;
        }

        // don't do this if hitpoint is on edge
        if(width != 0 && height != 0)
        {
            // current terrain splat data
            splat = targetT.terrainData.GetAlphamaps(newx, newz, width, height);

            for(int k = 0; k < height; k++)
            {
                for(int j = 0; j < width; j++)
                {
                    // this makes the ice look more deformed instead of square
                    if( (j-halfw) == 0 || Math.Abs(j-halfw) < counter)
                    {
                        for(int i = 0; i < targetT.terrainData.terrainLayers.Length; i++)
                        {
                            if(i == layer)
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

                if(k >= halfh)
                {
                    counter--;
                }
                else
                {
                    counter++;
                }
            }

            targetT.terrainData.SetAlphamaps(newx, newz, splat);

            if(layer == 2)
            {
                // remove ice after a certain amount of time
                StartCoroutine(RemoveIce(targetT, "TerrainBackups/" + targetT.terrainData.name + "_backup", newx, newz, width, height));
            }
        }
    }

    IEnumerator RemoveIce(Terrain t, String dataName, int x, int z, int width, int height)           
    {
        yield return new WaitForSeconds(60);
        // load the original terraindata
        orig = Resources.Load<TerrainData>(dataName);

        // set the patch of ice that was made previously to be the same terrain that is in the original
        t.terrainData.SetAlphamaps(x, z, orig.GetAlphamaps(x, z, width, height));
    }

    // uses the world to terrain coordinates to determine if the player is currently standing over an ice texture
    public bool CheckIce(int x, int z)
    {
        // this check accounts for boundary cases since we can't do a one offset at the last element
        if(x != 512 && z != 512)
        {
            // samples the 1x1 splatmap of the desired coordinates 
            splat = targetT.terrainData.GetAlphamaps(x, z, 1, 1);

            // if the player is standing on an ice layer return true
            if(splat[0, 0, iceLayer] == 1)
            {
                return true;
            }
        }  

        return false;
    }
    
}
