using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Material material;

    private Material[] myMaterials = new Material[5];
    void Start()
    {
        for(int i = 0; i < myMaterials.Length; i++) {
            myMaterials[i] = material;
        } 
        

    }

    // Update is called once per frame
    void Update()
    {
        if(SB_PickUp.isHoldingSpringBoots){
            SkinnedMeshRenderer renderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
            renderer.materials = myMaterials;
        }
    }
}
