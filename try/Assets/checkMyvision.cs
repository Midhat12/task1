using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    //how much sensitive we are about vision/line of sight
    public enum enmSenstivity {HIGH,LOW};


    //variable to check sensitivity
    public enmSenstivity sensitivity = enmSenstivity.HIGH;

    //Are we able to see the target right now
    public bool targetInSight = false;

    //feild of vision
    public float feildOfVision = 45.0f;

    //we need a reference to our target here as well
    private Transform target = null;
    //reference to our eyes yet
    public Transform myEyes = null;

    //my transform componnenet
    public Transform npcTransform = null;

    //my sphere collider
    private SphereCollider sphereCollider = null;

    //last known sighting of object
    public Vector3 lastKnownSighting = Vector3.zero;


    // Start is called before the first frame update


    private void Awake(){
        npcTransform = GetComponent<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
        lastKnownSighting = npcTransform.position;
        target = GetObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }
    bool inMyFeildofVision()
    {
        Vector3 dirToTarget = target.position - myEyes.position;
        float angle = Vector3.Angle(myEyes.forward,dirToTarget);
        if(angle <= fieldofVision){
            return true;
        }
              
        else
        {
            return false;
        }

    }
    bool ClearLineofSight()
    {
        RaycastHit hit;
        if(Physics.Raycast(myEyes.position, (target.position - myEyes.position).normalized,
        ot hit,sphereCollider.radius)){
            if(hit.transform.CompareTag("Player")){
                return true;
            }


        }
        return false;
    }

    void UpdateSight(){
        switch (sensitivity)
        {
            case enmSenstivity.HIGH:
               targetInSight = inMyFeildofVision() && ClearLineofSight();
               break;
            case enmSenstivity.Low:
                targetInSight = inMyFeildofVision() || ClearLineofSight();
            
        }
    }

    private void OnTriggerStay(Collider other){
        UpdatesSight();
        //Update last known sighting
        if(targetInSight)
           lastKnownSighting = target.position;
    }

    private void OnTriggerExit(Collider other){
        if(!other.CompareTag("Player")){
            return;
        }
        targetInSight= false;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
