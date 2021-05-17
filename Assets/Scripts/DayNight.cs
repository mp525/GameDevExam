using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    // Start is called before the first frame update
    public Material day;
    public Material night;

    public float daySpeed;

    private string darkShadow = "OC191B";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(500,5,500), Vector3.right, daySpeed*Time.deltaTime);
        transform.LookAt(new Vector3(500,5,500));
    }

    void FixedUpdate(){
        if(transform.position.y >= 0){
            //RenderSettings.skybox = day;
            daySpeed = 0.5f;
        }
        else{
            //RenderSettings.skybox = night;
            daySpeed = 2f;
        }
    }
}
