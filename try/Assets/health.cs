using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
   
   public float  HealthPoints{


       get{
           return healthPoints;

       } 

       set{
           healthPoints = value;

           if(healthPoints <= 0){
               Destroy(gameObject);
           }


       }
   }
   [SerializeField]
   private float healthPoints = 100f;
}
