using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonespawner : MonoBehaviour
{
    
    [SerializeField]
    private GameObject stonePrefab; 

    void Start()
    {
        InvokeRepeating("Spawn", 3.0f, 300.0f); //Kald hvert femte minut
    }

    void Spawn(){
        float count = 0.0f;
        Collider[] stoneColliders = Physics.OverlapSphere(transform.position, 5.0f);
        foreach (var stoneCollider in stoneColliders)
        {
            if(stoneCollider.gameObject.tag.Equals("Stone")){
                count++;
            }
        }

         if (count >= 3.0f){
            //Debug.Log("Max stones reached at " + transform.position.ToString());
            return;
        }
        else{
            Instantiate(stonePrefab, new Vector3(transform.position.x, transform.position.y,transform.position.z), Quaternion.identity);
        }

    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 5.0f);
    }
}
