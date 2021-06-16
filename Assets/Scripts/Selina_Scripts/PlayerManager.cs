using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
InventoryObject inventory;
InputManager inputManager;
PlayerMotion playerMotion;
CameraManager cameraManager;
public float hunger = 100f;
public float health = 100f;
[SerializeField] private float hungerOverTime = 100;




    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        cameraManager = FindObjectOfType<CameraManager>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        HungerOverTime();

        if(hunger <= 0){
            HealthOverTime();
            if(health <= 0){
                SceneManager.LoadScene(2);
            }
        }

    }

     public void SetHunger(float value){
        Debug.Log("Before: " + hunger);
        hunger = hunger + value;
        Debug.Log("After: " + hunger);
    }

    void HungerOverTime()
    {
        hunger -= (hungerOverTime * Time.deltaTime);
    }

    void HealthOverTime()
    {
        health -= (5.0f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerMotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }


    public void TakeDamage(float damage){
   float chanceOfHitting =Random.Range(1,4);
        
        if (chanceOfHitting==1)
        {
        health=health+-damage;    
        }
        
        
       if (health<1)
       {
            Death();
        
       }
        
    }

        public void Death(){
       
        SceneManager.LoadScene(2);
        //if human does this add some meat to human inventory
        
       
        
    }
}
