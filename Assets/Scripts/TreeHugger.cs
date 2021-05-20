using sc.terrain.vegetationspawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHugger : MonoBehaviour
{
    [SerializeField] private float m_destructionRange = 2.15f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            TerrainData terrain = Terrain.activeTerrain.terrainData;
            ArrayList instances = new ArrayList();

            foreach (TreeInstance tree in terrain.treeInstances)
            {
                
                var treePos = (Vector3.Scale(tree.position, terrain.size) + Terrain.activeTerrain.transform.position);
                float distance = (treePos - transform.position).magnitude;

                if (distance <= m_destructionRange)
                {
                    string prefabName = VegetationSpawner.Current.treeTypes[tree.prototypeIndex].name;
                    //Resources.Load(prefabName);
                    // You can spawn a prefab of the tree here! 
                    Instantiate(Resources.Load("Prefabs/Trees/"+prefabName), treePos, Quaternion.identity);
                    Debug.Log(VegetationSpawner.Current.treeTypes[tree.prototypeIndex].name);
                }
                else
                {
                    instances.Add(tree);
                }
            }
            terrain.treeInstances = (TreeInstance[])instances.ToArray(typeof(TreeInstance));
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), m_destructionRange);
    }
}
