using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    // Start is called before the first frame update
    public float daySpeed;

    private string darkShadow = "OC191B";

    public GameObject mainCamera;

    public AudioClip nightSound;
    public AudioClip daySound;
    public AudioClip ocean;

    private AudioSource source;

    private Vector3 center = new Vector3(500,5,500);

    void Start()
    {
        source = mainCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, Vector3.right, daySpeed * Time.deltaTime);
        transform.LookAt(center);
        
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(mainCamera.transform.position, center) >= 250){
            source.clip = ocean;
            if (!source.isPlaying)
            {
                source.Play();
                source.loop = true;
                source.volume = 0.2f;
            }
            return;
        }

        if (transform.position.y >= 100)
        {
            source.clip = daySound;
            if (!source.isPlaying)
            {
                source.Play();
                source.loop = true;
            }

            //Make days longer than nights
            daySpeed = 0.5f; 
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            RenderSettings.fog = false;
        }

        else
        {
            source.clip = nightSound;
            if (!source.isPlaying)
            {
                source.Play();
                source.loop = true;
            }

            /* if (transform.position.y <= 100 && transform.position.z <= 0){
                Debug.Log("Sunrise");
                RenderSettings.fogDensity -= 0.001f;
            } */

            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0, 0);
            //RenderSettings.ambientIntensity = 0.05f;
            //RenderSettings.reflectionIntensity = 0.05f;

            if (transform.position.y < 100 && transform.position.y > 0)
            {
                Debug.Log("Sunset");
                if (RenderSettings.fogDensity < 0.10f)
                {
                    RenderSettings.fogDensity += 0.001f;
                }
            }
            //Make nights shorter than days
            daySpeed = 2f; 

        }
    }
}
