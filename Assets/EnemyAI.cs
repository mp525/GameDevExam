 using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    // i will go back to this it aint good enough yet
    public enum AIState { Idle, Walking, growling, Running, Hit }
    public AIState currentState = AIState.Idle;
    public int awarenessArea = 10; //How far the friendly AI should detect the enemy
    public float walkingSpeed = 3.5f; //how Fast should it walk
    public float runningSpeed = 5f;//how Fast should it run
    public Animator animator;

    //Trigger collider that represents the awareness area
    SphereCollider c; 
    //NavMesh Agent
    NavMeshAgent agent;

    bool switchAction = false;
    float actionTimer = 0; //Timer duration till the next action
    Transform enemy;
    float range = 20; //How far the Deer have to run to resume the usual activities
    float multiplier = 1;
    bool reverseFlee = false; //In case the AI is stuck, send it to one of the original Idle points

    //Detect NavMesh edges to detect whether the AI is stuck
    Vector3 closestEdge;
    float distanceToEdge;
    float distance; //Squared distance to the enemy
    //How long the AI has been near the edge of NavMesh, if too long, send it to one of the random previousIdlePoints
    float timeStuck = 0;
    //Store previous idle points for reference
    List<Vector3> previousIdlePoints = new List<Vector3>(); 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.autoBraking = true;

        c = gameObject.AddComponent<SphereCollider>();
        c.isTrigger = true;
        c.radius = awarenessArea;

        //Initialize the AI state
        currentState = AIState.Idle;
        actionTimer = Random.Range(0.1f, 2.0f);
        SwitchAnimationState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        //Wait for the next course of action
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
        }
        else
        {
            switchAction = true;
        }

        if (currentState == AIState.Idle)
        {
            if(switchAction)
            {
                if (enemy)
                {
                    //Run to
                    
                    agent.SetDestination(enemy.transform.position);
                    currentState = AIState.Running;
                    SwitchAnimationState(currentState);      
                    
                  
                     
                }
                else
                {
                    //No enemies nearby, start eating
                    actionTimer = Random.Range(2, 5);

                    currentState = AIState.growling;
                    SwitchAnimationState(currentState);

                    //Keep last 5 Idle positions for future reference
                    previousIdlePoints.Add(transform.position);
                    if (previousIdlePoints.Count > 5)
                    {
                        previousIdlePoints.RemoveAt(0);
                    }
                }
            }
        }
        else if (currentState == AIState.Walking)
        {
            //Set NavMesh Agent Speed
            agent.speed = walkingSpeed;

            // Check if we've reached the destination
            if (DoneReachingDestination())
            {
                currentState = AIState.Idle;
            }
        }
        else if (currentState == AIState.growling)
        {
            if (switchAction)
            {
                //Wait for current animation to finish playing
                if(!animator || animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) > 0.99f)
                {
                    //Walk to another random destination
                    agent.destination = RandomNavSphere(transform.position, Random.Range(3, 10));
                    
                    currentState = AIState.Walking;
                    SwitchAnimationState(currentState);
                }
            }
        }
        else if (currentState == AIState.Running)
        {
            //Set NavMesh Agent Speed
            agent.speed = runningSpeed;

            //Run to enemy
            if (enemy)
            {
                if (reverseFlee)
                {
                    if (DoneReachingDestination() && timeStuck < 0)
                    {
                        
                            SwitchAnimationState(AIState.Hit);
                        
                    }
                    else
                    {
                        timeStuck -= Time.deltaTime;
                    }
                }
                else
                {
                    Vector3 runTo =  enemy.position;
                    distance = (transform.position - enemy.position).sqrMagnitude;


                    if (enemy)
                    {
                        agent.SetDestination(runTo);
                        
                    }
                    else
                    {
                        enemy = null;
                    }
                }
                
                //Temporarily switch to Idle if the Agent stopped
                if(agent.velocity.sqrMagnitude < 0.1f * 0.1f)
                {
                    SwitchAnimationState(AIState.Idle);
                }
                 if(distance<4)
                {
                    SwitchAnimationState(AIState.Hit);
                }
                else
                {
                    SwitchAnimationState(AIState.Running);
                }

                
                
            }
            else
            {
                //Check if we've reached the destination then stop running
                if (DoneReachingDestination())
                {
                   actionTimer = Random.Range(2f, 5f);
                    currentState = AIState.Hit;
                    SwitchAnimationState(AIState.Hit);
                    
                     
                }
            }
        }

        switchAction = false;
    }

    bool DoneReachingDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    //Done reaching the Destination
                    return true;
                }
            }
        }

        return false;
    }

    void SwitchAnimationState(AIState state)
    {
        //Animation control
        if (animator)
        {
            animator.SetBool("isGrowling", state == AIState.growling);
            animator.SetBool("isIdle", state == AIState.Idle);
            animator.SetBool("isRunning", state == AIState.Running);
            animator.SetBool("isWalking", state == AIState.Walking);
            animator.SetBool("isHit", state == AIState.Hit);
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!enemy){
             enemy = other.transform;

        actionTimer = Random.Range(0.24f, 0.8f);
        currentState = AIState.Running;
        SwitchAnimationState(currentState);
        }
        
       
    }
    private void OnTriggerStay(Collider other) {
        if (enemy)
        {
            
        if (distance<3)
        {
            
            SwitchAnimationState(AIState.Hit);
        }     
        }
       
    }
     
   
}


