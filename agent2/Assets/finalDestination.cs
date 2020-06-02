using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class finalDestination : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agentt = null;
    public Transform Destination = null;
    // Start is called before the first frame update
    void Start()
    {
        agentt = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agentt.SetDestination(Destination.position);
        
    }
}
