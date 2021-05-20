using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision){
        Debug.Log("Food collision with: " + collision.gameObject.name);
        if(collision.gameObject.tag.Equals("Player")){
            Debug.Log("TODO: Add to hunger bar or pick up...");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
