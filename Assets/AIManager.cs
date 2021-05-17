using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{


public float health = 100f;
HealthBarAI healthBarAI;




    private void Awake()
    {
       healthBarAI=GetComponent<HealthBarAI>(); 
      

    }

    private void Update()
    {
       
       

    }


}
