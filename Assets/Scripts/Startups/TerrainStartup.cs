using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.tag = Constants.terrainGameObjectTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
