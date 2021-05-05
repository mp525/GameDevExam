using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRemoval : MonoBehaviour
{
    // Start is called before the first frame update

    private Terrain terrain;
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        Debug.Log(terrain.terrainData.treeInstanceCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
