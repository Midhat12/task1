using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//Khadeeja Gilani
public class Enemy_FSM : MonoBehaviour
{	
	public enum enemyState{PATROL, CHASE, ATTACK};
	

	public enemyState CurrentState
	{
		
		get{return currentState;}
		set
		{	
			//StopAllCoroutines();
			currentState = enemyState.PATROL;
			
			switch(currentState)
			{
				case enemyState.PATROL:
					StartCoroutine(EnemyPatrol());
					break;
				case enemyState.CHASE:
					StartCoroutine(EnemyChase());
					break;
				case enemyState.ATTACK:
					StartCoroutine(EnemyAttack());
					break;
				
			}
		}
	}
	
	[SerializeField] private enemyState currentState;	

	private CheckMyVision checkMyVision;

	private NavMeshAgent agent = null;

	private Transform playerTransform = null;
	
	private Transform patrolDestination = null;

	private Health playerHealth = null;
	
	public float maxDamage = 10.0f;

	private void Awake()
	{
		
		checkMyVision = GetComponent<CheckMyVision>();
		
		agent = GetComponent<NavMeshAgent>();
		
		playerHealth = GameObject.FindGameObjectWithTag("Player1").GetComponent<Health>();
		
		playerTransform = playerHealth.GetComponent<Transform>();
	}

    // Start is called before the first frame update
    void Start()
    {

	GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destiny");
	
	patrolDestination = destinations[Random.Range(0, destinations.Length)].GetComponent<Transform>();
	
        currentState = enemyState.PATROL;
    }

	public IEnumerator EnemyPatrol()
	{
		Debug.Log("1..Started Patrolling");
		while(currentState == enemyState.PATROL)
		{
			checkMyVision.sensitivity = CheckMyVision.eSensitivity.HIGH;
			agent.isStopped = false;
			Debug.Log("Started Patrolling");
			agent.SetDestination(patrolDestination.position);
			while(agent.pathPending)
				yield return null;

			if(checkMyVision.targetInSight)
			{
				Debug.Log("Started Chasing");
				agent.isStopped = true;
				currentState = enemyState.CHASE;
				yield break;
			}
			yield return null;
		}
		
	}
	
	public IEnumerator EnemyChase()
	{
		Debug.Log("Chasing");
		while(currentState == enemyState.CHASE)
		{
			checkMyVision.sensitivity = CheckMyVision.eSensitivity.LOW;
			agent.isStopped = false;
			agent.SetDestination(checkMyVision.lastKnownLocation);

			while(agent.pathPending)
			{
				yield return null;
			}
			
			if(agent.remainingDistance <= agent.stoppingDistance)
			{
				agent.isStopped = true;
				if(!checkMyVision.targetInSight)
				{
					currentState = enemyState.PATROL;
				}
				else
				{
					currentState = enemyState.ATTACK;
				}
				yield break;
			}
			yield return null;
		}

	}

	public IEnumerator EnemyAttack()
	{
		Debug.Log("Attacking1");
		while(currentState == enemyState.ATTACK)
		{
			Debug.Log("Attacking");
			agent.isStopped = false;
			agent.SetDestination(playerTransform.position);

			while(agent.pathPending)
			{
				yield return null;
			}
			
			if(agent.remainingDistance > agent.stoppingDistance)
			{
				currentState = enemyState.CHASE;
				yield break;
			}
			else
			{
				playerHealth.healthPoints -= maxDamage * Time.deltaTime;
				
			}
			yield return null;
		}
		yield break;
	}
}
