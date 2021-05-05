using sc.terrain.vegetationspawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRespawner : MonoBehaviour
{
    private void OnDestroy() => GetComponent<VegetationSpawner>().Respawn();

}
