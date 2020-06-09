using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//Khadeeja Gilani
public class CheckMyVision : MonoBehaviour
{
	public enum eSensitivity {HIGH, LOW};
	public eSensitivity sensitivity = eSensitivity.HIGH;
	public bool targetInSight = false;
	public float fieldOfVision = 45.0f;
	private Transform target = null;
	public Transform myEyes = null;
	public Transform npcTransform = null;
	private SphereCollider sphereCollider = null;
	
	public Vector3 lastKnownLocation = Vector3.zero;
	
	public void Awake()
	{	
		Debug.Log("Awake cmv");
		npcTransform = GetComponent<Transform>();
		sphereCollider = GetComponent<SphereCollider>();
		lastKnownLocation = npcTransform.position;
		target = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();		
	}

	bool InMyFieldOfVision()
	{
		Vector3 dirToTarget = target.position - myEyes.position;
		float angle = Vector3.Angle(myEyes.forward, dirToTarget);
		if(angle <= fieldOfVision){
			Debug.Log("In my field of vision new!");
			return true;
		}
		else{
			Debug.Log("Not in my field of vision!");
			return false;
		}
	}

	bool ClearLineOfSight()
	{
		RaycastHit hit;
		if(Physics.Raycast(myEyes.position, (target.position - myEyes.position).normalized, out hit, sphereCollider.radius))
		{
			Debug.Log("In my field of vision! AMB");
			if(hit.transform.CompareTag("Player1"))
			{
				Debug.Log("In my field of vision!");
				return true;
			}	
			
		}
		Debug.Log("Not in my field of vision!!");
		return false;
	}
	
	void UpdateSight()
	{
		switch (sensitivity)
		{
			case eSensitivity.HIGH:
				targetInSight = InMyFieldOfVision() && ClearLineOfSight();
				break;
			case eSensitivity.LOW:
				targetInSight = InMyFieldOfVision() || ClearLineOfSight();
				break;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		UpdateSight();
		if(targetInSight)
		{
			Debug.Log("the player");
			lastKnownLocation = target.position;
			
			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(!other.CompareTag("Player1"))
		{	
			
			return; 
		}
		
		targetInSight = false;
		
	}
	
    	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player1"))
		{	
			lastKnownLocation = target.position;
			targetInSight = true;
			
			return; 
		}
		
		
		
	}
}
