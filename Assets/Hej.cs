using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hej : MonoBehaviour
{
    private CapsuleCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c){
        Debug.Log("Collision with " + c.gameObject.name);
    }
}
