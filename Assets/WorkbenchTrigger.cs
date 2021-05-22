using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchTrigger : MonoBehaviour
{
    
    public Animator animator;
    public GameObject go;
    bool near=false;
    float distance;
    void Update()
    {
        distance = (transform.position - go.transform.position).sqrMagnitude;
        if(distance<35){
            near=true;
        }
        if(near){
                

            animator.SetBool("Near",true);
        }
    }
}
