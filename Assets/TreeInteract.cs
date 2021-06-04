using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteract : MonoBehaviour
{
    private float treeHealth = 3.0f;

    [SerializeField]
    private GameObject logPrefab;
    
    void OnCollisionEnter(Collision collision){
        string collisionName = collision.gameObject.name;
        Debug.Log("Collision with " + collisionName);

        if(collision.gameObject.tag.Equals("Axehead")){ //Muligvis øksehoved i fremtiden
            gameObject.GetComponent<AudioSource>().Play();
            if(treeHealth == 1.0f){
                Destroy(gameObject);
                Instantiate(logPrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            }
            else{
                treeHealth--;
            }
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
