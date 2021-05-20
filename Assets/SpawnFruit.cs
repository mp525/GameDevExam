using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject fruitPrefab;

    void Start()
    {
        InvokeRepeating("Spawn", 3.0f, 300.0f);
    }

    void Spawn(){
        Instantiate(fruitPrefab, new Vector3(transform.position.x, transform.position.y,transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
