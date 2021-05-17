using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    
    Transform cam;
    // Update is called once per frame
    private void Start() {
        cam= GameObject.FindWithTag("MainCamera").transform;
    }
    void LateUpdate()
    {
        
        transform.LookAt(transform.position+cam.forward);
    }
}
