using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_npc : MonoBehaviour
{
    //enum for krrping states
    public enum enemy_state {PATROL,CHASE,ATTACK};

    [SerializeField]
    private enemy_state currentstate;
    private checVision checkmyVision;
    private UnityEngine.AI.NavMeshAgent agent = null;
    private Transform playertransform = null;
    private Transform patrolDestination = null;

    private health playerHealth = null;
    public float maxDamage =10f;
 

    public enemy_state CurrentState{
        get {
            return currentstate;
        }
        set{
            currentstate=value;
            StopAllCoroutines();
            switch(currentstate){
                
                case enemy_state.PATROL:
                StartCoroutine(EnemyPatrol());
                break;
                case enemy_state.CHASE:
                StartCoroutine(EnemyChase());
                break;
                case enemy_state.ATTACK:
                StartCoroutine(EnemyAttack());
                break;
            }
        }
    }

   

    private void Awake(){
        checkmyVision=GetComponent<checVision>();
        agent=GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<health>();
        playertransform = playerHealth.GetComponent<Transform>();
    }

    // Start is called before the first frame update

    void Start()
    {
        //GameObject[] destination = GameObject.FindGameObjectsWithTag("Dest");
        patrolDestination= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentstate= enemy_state.CHASE;

        
    }
    public IEnumerator EnemyPatrol(){
        while(currentstate==enemy_state.PATROL){
            checkmyVision.sensitivity = checVision.Sensitivity.HIGH;
            agent.isStopped=false;
            agent.SetDestination(patrolDestination.position);

            while(agent.pathPending)
                yield return null;
            
            if(checkmyVision.targetInSight){
                Debug.Log("Found you, changing to CHASE state");
                agent.isStopped=true;
                currentstate=enemy_state.CHASE;
                yield break;
            }
            yield return null;
        }
        
    }
    public IEnumerator EnemyChase(){
        while(currentstate == enemy_state.CHASE){
            checkmyVision.sensitivity = checVision.Sensitivity.LOW;
            agent.isStopped = false;
            agent.SetDestination(checkmyVision.lastknownSight);


            while(agent.pathPending){
                yield return null;
            }
          //while chasing we need to keep checkingif we reached
          if(agent.remainingDistance <= agent.stoppingDistance){
              agent.isStopped = true;

              if(!checkmyVision.targetInSight){
                  currentstate =enemy_state.PATROL;

              }
              else
              {
                  print ("Chasing to Attack");
                  currentstate =enemy_state.ATTACK;
              }
              yield break;

          }
          yield return null;
        }
        
    }
    public IEnumerator EnemyAttack(){
         print ("Attacking enemy");
        while (currentstate == enemy_state.ATTACK)
        {
            agent.isStopped = false;
            agent.SetDestination(playertransform.position);
            while(agent.pathPending)
                yield return null;
             if(agent.remainingDistance > agent.stoppingDistance){
                 print ("Attack to Chasing");
                 CurrentState = enemy_state.CHASE;

             }
             else
             {
                 playerHealth.HealthPoints -= maxDamage * Time.deltaTime;

             }

            yield return null;

           


        }
        yield break;
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}