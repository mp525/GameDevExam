using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class State
{
    public enum STATE{
        IDLE,PATROL,RunAway,SLEEP
    };
    public enum EVENT{
        ENTER,UPDATE,EXIT
    };
    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator animator;
    protected Transform enemy;
    protected State nextState;
    protected NavMeshAgent agent;
    float visDistance =10.0f;
    float visAngle= 30.0f;

    public State(GameObject _npc, NavMeshAgent _agent,Animator _anim,Transform _enemy){
        npc=_npc;
        agent=_agent;
        animator=_anim;
        stage=EVENT.ENTER;
        enemy=_enemy;
    }
    public virtual void Enter(){stage=EVENT.UPDATE;}
    public virtual void Update(){stage=EVENT.UPDATE;}
    public virtual void Exit(){stage=EVENT.EXIT;}

    public State Process()
    {
        if (stage==EVENT.ENTER)Enter();
        if(stage==EVENT.UPDATE)Update();
        if(stage==EVENT.EXIT){
            
            Exit();
            return nextState;
        }
        return this;
    }

}

public class Idle:State{
    public Idle(GameObject _npc, NavMeshAgent _agent,Animator _anim,Transform _player)
    :base (_npc,_agent,_anim,_player)
    {
        name=STATE.IDLE;
    }
    public override void Enter()
    {
        
        animator.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if(Random.Range(0,100)<10)
        {
           
            nextState=new Patrol(npc, agent,animator,enemy);
            base.Exit();
        }
        
    }
    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Patrol:State{
    int currentIndex=-1;
    
    public Patrol(GameObject _npc, NavMeshAgent _agent,Animator _anim,Transform _player)
    :base (_npc,_agent,_anim,_player)
    {
        name=STATE.PATROL;
        agent.speed=2;
        agent.isStopped=false;
    }
    public override void Enter()
    {
       
        currentIndex=0;
        animator.SetTrigger("isWalking");
        base.Enter();
    }
    public override void Update()
    {
        if (Vector3.Distance(agent.transform.position,enemy.transform.position)<5)
        {
             nextState=new RunAway(npc, agent,animator,enemy);
             base.Exit();
        }
        if(agent.remainingDistance<1)
        {
            if (currentIndex>=GameEnvironment.Singleton.Checkpoints.Count-1)
            currentIndex=0;
            else
            currentIndex++;
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);  
        }
        
    }
    public override void Exit()
    {
        
        animator.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class RunAway:State{
    int currentIndex=-1;
    
    public RunAway(GameObject _npc, NavMeshAgent _agent,Animator _anim,Transform _enemy)
    :base (_npc,_agent,_anim,_enemy)
    {
        name=STATE.PATROL;
        agent.speed=4;
        agent.isStopped=false;
    }
    public override void Enter()
    {
        Debug.Log("Run Away state");
        currentIndex=0;
        animator.SetTrigger("isRunning");
        base.Enter();
    }
    public override void Update()
    {
        // transform.position + ((transform.position - enemy.position) * multiplier
        if(agent.remainingDistance<1)
        {
            //run to the furthest point away from enemy 
            //calc distance
            float distance=0;
            var point=enemy.position;
            for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count-1; i++)
            {
                var disToPoint=Vector3.Distance(GameEnvironment.Singleton.Checkpoints[i].transform.position,enemy.transform.position);
                if (disToPoint>distance)
                {
                    distance=disToPoint;
                    point=GameEnvironment.Singleton.Checkpoints[i].transform.position;
                }
            }

           
            agent.SetDestination(point);  
        }
    }
    public override void Exit()
    {
        animator.ResetTrigger("isRunning");
        base.Exit();
    }
}