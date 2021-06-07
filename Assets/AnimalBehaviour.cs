using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalBehaviour : MonoBehaviour
{
        public enum AIState { Idle, Walking, Jumping, Running }
    public Animator animator;

    BehaviourTree tree;
    public GameObject player;
    public GameObject tree1;
    public enum ActionState {IDLE, Working};
    ActionState state= ActionState.IDLE;
    public AIState currentState = AIState.Idle;

    NavMeshAgent agent;

    [Range (0,1000)]
    public double lonelyness;
    Node.Status treeStatus=Node.Status.RUNNING;
    void Start()
    {
        agent=this.GetComponent<NavMeshAgent>();
     tree=new BehaviourTree();
     Sequence runAway=new Sequence("Run Somewhere");  
     //Leaf=Actions 
     Leaf lonely=new Leaf("lonely",HasLonelyness);
     Leaf becomingLessLonely=new Leaf("less lonely",NearPlayer);
     Leaf pee= new Leaf("pee",GoToTree1);

     Selector choose = new Selector("What to do");
    
     runAway.addChild(pee);
     runAway.addChild(lonely);
     choose.addChild(pee);
     choose.addChild(lonely);
     runAway.addChild(choose);
     
     tree.addChild(runAway);
     
     tree.printTree();
     agent.SetDestination(player.transform.position);
     tree.Process();
    }
    public Node.Status HasLonelyness(){
        if (lonelyness>=900)
         return GoToLocation(player.transform.position);
        return Node.Status.FAILURE;
    }
    public Node.Status NearPlayer(){
        if(lonelyness>=100){
        state=ActionState.IDLE;
         return Node.Status.RUNNING;
        }
         else{
            return Node.Status.SUCCESS;
        }
        
    }
    
    public Node.Status GoToTree1(){
        if(lonelyness>=100){
            return Node.Status.FAILURE;
        }
        return GoToLocation(tree1.transform.position);
    }

    Node.Status GoToLocation(Vector3 des){
        float distanceToTarget= Vector3.Distance(des,this.transform.position);
        if(state==ActionState.IDLE){
            agent.SetDestination(des);
            state=ActionState.Working;
            currentState=AIState.Walking;
        }
        else if(Vector3.Distance(agent.pathEndPosition,des)>=2){
            state=ActionState.IDLE;
            currentState=AIState.Idle;
            return Node.Status.FAILURE;
        }
        else if(distanceToTarget<2){
            state=ActionState.IDLE;
            currentState=AIState.Idle;
            Debug.Log("Reached");
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer= Vector3.Distance(player.transform.position,this.transform.position);

        if(treeStatus!=Node.Status.SUCCESS){
            treeStatus=tree.Process();
        }
        if (distanceToPlayer>10&&lonelyness<1000)
        {
            lonelyness+=1;    
        } 
        
        if(distanceToPlayer<10&&lonelyness>0){
            lonelyness-=1;
        }
        SwitchAnimationState(currentState);
    }
    void SwitchAnimationState(AIState state)
    {
        //Animation control
        if (animator)
        {
            animator.SetBool("isJumping", state == AIState.Jumping);
            animator.SetBool("isIdle", state == AIState.Idle);
            animator.SetBool("isRunning", state == AIState.Running);
            animator.SetBool("isWalking", state == AIState.Walking);
        }
    }
}
