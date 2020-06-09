using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinNation : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent=null;
    public  Transform dest = null;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(dest.position);
    }
}
