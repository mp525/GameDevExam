  using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class FriendlyAI : MonoBehaviour
{
    // i will go back to this it aint good enough yet
    public enum AIState { Idle, Walking, jumping, Running }
    public AIState currentState = AIState.Idle;
    public int awarenessArea = 10; //How far the friendly AI should detect the enemy
    public float walkingSpeed = 3.5f; //how Fast should it walk
    public float runningSpeed = 5f;//how Fast should it run
    public Animator animator;

    //Trigger collider that represents the awareness area
    SphereCollider c; 
    NavMeshAgent agent;

    bool switchAction = false;
    float actionTimer = 0; //Timer duration till the next action
    Transform enemy;
    float range = 20; //How far the Animal have to run to resume the usual activities
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

    AIManager aIManager;
    float health=100;
    HealthBarAI healthBarAI;
    //public GameObject food;

    bool firstTime=true;
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
        
        healthBarAI=GetComponentInChildren<HealthBarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health<1)
        {
            Death();
        }
        //Wait for the next course of action
        //The indication of thinking even tho its not i guess
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
            //if switch action is called do one of two things 
            //1 run to a random place if enemy near
            //2 start jumping because you are happy there is no enemies
            if(switchAction)
            {
                if (enemy)
                {
                    //Run away
                    agent.SetDestination(RandomNavSphere(transform.position, Random.Range(1, 2.4f)));
                    currentState = AIState.Running;
                    SwitchAnimationState(currentState);
                }
                else
                {
                    //No enemies nearby, start jumping
                    actionTimer = Random.Range(2, 5);
                    currentState = AIState.jumping;
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
        //better stop jumping and walk to a different place
        else if (currentState == AIState.jumping)
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

            //Run away
            if (enemy)
            {
                if (reverseFlee)
                {
                    if (DoneReachingDestination() && timeStuck < 0)
                    {
                        reverseFlee = false;
                    }
                    else
                    {
                        timeStuck -= Time.deltaTime;
                    }
                }
                else
                {
                    Vector3 runTo = transform.position + ((transform.position - enemy.position) * multiplier);
                    distance = (transform.position - enemy.position).sqrMagnitude;

                    //Find the closest NavMesh edge
                    NavMeshHit hit;
                    if (NavMesh.FindClosestEdge(transform.position, out hit, NavMesh.AllAreas))
                    {
                        closestEdge = hit.position;
                        distanceToEdge = hit.distance;
                        //Debug.DrawLine(transform.position, closestEdge, Color.red);
                    }

                    if (distanceToEdge < 1f)
                    {
                        if(timeStuck > 1.5f)
                        {
                            //Maybe problem with this
                            if(previousIdlePoints.Count > 0)
                            {
                                runTo = previousIdlePoints[Random.Range(0, previousIdlePoints.Count - 1)];
                                reverseFlee = true;
                            } 
                        }
                        else
                        {
                            timeStuck += Time.deltaTime;
                        }
                    }

                    if (distance < range * range)
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
                    actionTimer = Random.Range(1.4f, 3.4f);
                    currentState = AIState.jumping;
                    SwitchAnimationState(AIState.jumping);
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
            animator.SetBool("isJumping", state == AIState.jumping);
            animator.SetBool("isIdle", state == AIState.Idle);
            animator.SetBool("isRunning", state == AIState.Running);
            animator.SetBool("isWalking", state == AIState.Walking);
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
        //Make sure the Player instance has a tag "Player"
        if (other.CompareTag("Player"))
            return;

        enemy = other.transform;

        //make something here so they might get eaten by tiger and die
        
        actionTimer = Random.Range(0.24f, 0.8f);
        currentState = AIState.Running;
        SwitchAnimationState(currentState);
    }
    public void Death(){
        if (firstTime)
        {
        gameObject.SetActive(false);
        firstTime=false;   
        Debug.Log(gameObject.name+" died"); 
        }
        //if human does this add some meat to human inventory
        
       
        
    }
    public void TakeDamage(float damage){
   
        health=health+-damage;
        healthBarAI.TakeDamage(damage);
        healthBarAI.SetCurrentHealth(health);                                         
       if (health<1)
       {
            Death();
        
       }
        
    }
   
}

