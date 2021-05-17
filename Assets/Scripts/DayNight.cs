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

        if (transform.position.y >= 300)
        {
            source.clip = daySound;
            if (!source.isPlaying)
            {
                source.Play();
                source.loop = true;
            }

            //Make days longer than nights
            //daySpeed = 0.5f; 
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

            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0, 0);
            //RenderSettings.ambientIntensity = 0;
            //RenderSettings.reflectionIntensity = 0;

            if (transform.position.y < 300 && transform.position.y > 0)
            {
                Debug.Log("Sun's getting real low...");
                if (RenderSettings.fogDensity < 0.15f)
                {
                    RenderSettings.fogDensity += 0.001f;
                }
            }
            //Make nights shorter than days
            //daySpeed = 2f; 
            //RenderSettings.ambientIntensity = 0;
            //RenderSettings.reflectionIntensity = 0;

        }
    }
}
