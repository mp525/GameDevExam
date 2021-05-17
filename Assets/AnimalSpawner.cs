using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimalSpawner : MonoBehaviour
{
    public int amount;
    float left;
    public List<GameObject> goList;
    public List<Transform> spawnPoints;

    private void Start() {
       left=amount;
       
    }
    private void Update() {
        Spawn();
    }
    void Spawn(){
    
        if(left>0){

        
        foreach (var item in goList)
        {
            //get nav mesh area
        int random=Random.Range(0,spawnPoints.Capacity);

         Instantiate(item,spawnPoints[random]);   
        }
        left=left-1;
        }
    }
    
    

}
