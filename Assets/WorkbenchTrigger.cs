using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchTrigger : MonoBehaviour
{
    
    public Animator animator;
    public GameObject go;
    bool near=false;
    float distance;

    void Start(){
        go = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        distance = (transform.position - go.transform.position).sqrMagnitude;
        if(distance<5.0f){
            near=true;
        }
        if(near){
                Debug.Log("Distance: " + distance + ". In near.");

            animator.SetBool("Near",true);
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 5.0f);
    }
}
