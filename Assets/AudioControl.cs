using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioClip sound;
    
    public AudioClip ocean;

    private Vector3 center = new Vector3(500, 5, 500);

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         if(Vector3.Distance(gameObject.transform.position, center) >= 250){
            gameObject.GetComponent<AudioSource>().clip = ocean;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<AudioSource>().loop = true;
        }
        else{
            //gameObject.GetComponent<AudioSource>().Play();
            //gameObject.GetComponent<AudioSource>().loop = true;
        } 
        
    }
}
