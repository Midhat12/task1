using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    public float Speed;

    private Rigidbody rbi;

   

    void Start () {
        rbi = GetComponent<Rigidbody>();
	}

	void Update ()
    {
        float HorizontalMovment = Input.GetAxis("Horizontal");
        float VerticalMovment = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);

        rbi.AddForce(move * Speed);
    }

   
}
