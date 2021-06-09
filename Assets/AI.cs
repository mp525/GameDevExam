using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;
    public Transform enemy;
    State currentState;

    float health = 100;
    HealthBarAI healthBarAI;
    // Start is called before the first frame update
    void Start()
    {
        agent=this.GetComponent<NavMeshAgent>();
        animator=this.GetComponent<Animator>();
        currentState=new Idle(this.gameObject,agent,animator,enemy);
         healthBarAI = GetComponentInChildren<HealthBarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState=currentState.Process();
    
    }
public void Death()
    {
        
            gameObject.SetActive(false);
            
            Debug.Log(gameObject.name + " died");
           
            
        
        
    }
    public void TakeDamage(float damage)
    {

        health = health + -damage;
        healthBarAI.TakeDamage(damage);
        healthBarAI.SetCurrentHealth(health);
        if (health < 1)
        {
            Death();
        }
    }
}
