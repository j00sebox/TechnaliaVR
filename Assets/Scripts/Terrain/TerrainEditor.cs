using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace TerrainTools
{
    public enum TerrainType
    {
        NONE = -1,
        DIRT,
        ROCK,
        ICE,
        WEB
    }

    public class TerrainEditor : MonoBehaviour
    {
        [SerializeField]
        private int _sampleWidth = 10;

        [SerializeField]
        private int _sampleHeight = 10;

        private Terrain _targetT;

        private float[,] _heightMap;

        private int _terrainHeightMapWidth;

        private int _terrainHeightMapHeight;

        private TerrainData _terrainData;

        private TerrainData _orig;

        private float[,,] _splat;

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
            if(_targetT)
            {
                return _targetT.terrainData;
            }

            return default(TerrainData);
        }

        public Terrain GetTerrain()
        {
            if (_targetT)
            {
                return _targetT;
            }

            return default(Terrain);
        }

        public void SetEditValues(Terrain t)
        {
            _targetT = t;
            _terrainData = GetTerrainData();
            _heightMap = GetHeightMap();
            _terrainHeightMapHeight = GetTerrainHeight();
            _terrainHeightMapWidth = GetTerrainWidth();
        }

        public float[,] GetHeightMap()
        {
            if(_targetT)
            {
                return _targetT.terrainData.GetHeights(0, 0, _targetT.terrainData.heightmapResolution, _targetT.terrainData.heightmapResolution);
            }

            return default(float[,]);
        }

        public int GetTerrainWidth()
        {
            if (_targetT)
            {
                return _targetT.terrainData.heightmapResolution;
            }
            return 0;
        }
        public int GetTerrainHeight()
        {
            if (_targetT)
            {
                return _targetT.terrainData.heightmapResolution;
            }
            return 0;
        }

        public Vector3 GetTerrainSize()
        {
            if (_targetT)
            {
                return _targetT.terrainData.size;
            }
            return Vector3.zero;
        }

        // turn the world coordinates into the indicies of the height/aplha map
        public void GetCoords(RaycastHit hit, out int x, out int z)
        {
            x = (int)( ( ( hit.point.x - _targetT.GetPosition().x ) / _targetT.terrainData.size.x ) * _targetT.terrainData.heightmapResolution );
            z = (int)( ( ( hit.point.z - _targetT.GetPosition().z ) / _targetT.terrainData.size.z ) * _targetT.terrainData.heightmapResolution );
        }

        public void GetCoords(Vector3 hit, out int x, out int z)
        {
            x = (int)( ( ( hit.x - _targetT.GetPosition().x ) / _targetT.terrainData.size.x ) * _targetT.terrainData.heightmapResolution );
            z = (int)( ( ( hit.z - _targetT.GetPosition().z ) / _targetT.terrainData.size.z ) * _targetT.terrainData.heightmapResolution );
        }

        public void ModifyTerrain(int x, int z, TerrainType type)
        {

            int width, height;
            // this keeps track of how many cells in each row of the ice patch shouldn't be changed
            int counter = 2;

            // if this is true the entire sample width can be taken
            if(x + _sampleWidth <= 512)
            {
                width = _sampleWidth;
            }
            else // else the sample width the difference between the edge of the terrain and the hit coordinate
            {
                width = 512 - x;
            }

            // same thing for sample height
            if(z + _sampleHeight <= 512)
            {
                height = _sampleHeight;
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
                _splat = _targetT.terrainData.GetAlphamaps(newx, newz, width, height);

                for(int k = 0; k < height; k++)
                {
                    for(int j = 0; j < width; j++)
                    {
                        // this makes the ice look more deformed instead of square
                        if( (j-halfw) == 0 || Math.Abs(j-halfw) < counter)
                        {
                            for(int i = 0; i < _targetT.terrainData.terrainLayers.Length; i++)
                            {
                                if(i == (int)type)
                                {
                                    _splat[k, j, i] = 1;
                                }
                                else
                                {
                                    _splat[k, j, i] = 0;
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

                _targetT.terrainData.SetAlphamaps(newx, newz, _splat);

                if((int)type == 2)
                {
                    // remove ice after a certain amount of time
                    StartCoroutine(RemoveIce(_targetT, "TerrainBackups/" + _targetT.terrainData.name + "_backup", newx, newz, width, height));
                }
            }
        }

        IEnumerator RemoveIce(Terrain t, String dataName, int x, int z, int width, int height)           
        {
            yield return new WaitForSeconds(60);
            // load the original terraindata
            _orig = Resources.Load<TerrainData>(dataName);

            // set the patch of ice that was made previously to be the same terrain that is in the original
            t.terrainData.SetAlphamaps(x, z, _orig.GetAlphamaps(x, z, width, height));
        }

        // uses the world to terrain coordinates to determine if the player is currently standing over an ice texture
        public bool CheckIce(int x, int z)
        {
            // this check accounts for boundary cases since we can't do a one offset at the last element
            if(x < 512 && z < 512)
            {
                // samples the 1x1 splatmap of the desired coordinates 
                _splat = _targetT.terrainData.GetAlphamaps(x, z, 1, 1);

                // if the player is standing on an ice layer return true
                if(_splat[0, 0, (int)TerrainType.ICE] == 1)
                {
                    return true;
                }
            }  

            return false;
        }

        // returns the layer that corresponds to the coordinates
        public bool CheckWebbed(int x, int z)
        {
            // this check accounts for boundary cases since we can't do a one offset at the last element
            if(x < 512 && z < 512)
            {
                // samples the 1x1 splatmap of the desired coordinates 
                _splat = _targetT.terrainData.GetAlphamaps(x, z, 1, 1);

                // if the player is standing on an ice layer return true
                if(_splat[0, 0, (int)TerrainType.WEB] == 1)
                {
                    return true;
                }
            }  

            return false;
        }

    }

}
