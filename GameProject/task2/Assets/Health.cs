using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//Khadeeja Gilani
public class Health : MonoBehaviour
{
	public float HealthPoints
	{
		get
		{
			return healthPoints;
		}
		set
		{
			healthPoints = value;

			if(HealthPoints <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
	[SerializeField] public float healthPoints = 100.0f;
}
