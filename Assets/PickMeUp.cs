using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMeUp : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider collider){
        Debug.Log("Branch collision trigger with: " + collider.name);
        
        if(collider.tag.Equals("Player")){
            Debug.Log("TODO: Inventory add wood...");
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
