using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float health = 5.0f;
    public float speed = 7.0f;



	// Use this for initialization
	void Start () { 


	}
	
	// Update is called once per frame
	void Update () {

    Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    Vector3 direction = input.normalized;

    Vector3 velocity = speed * direction;
    Vector3 moveAmount = velocity * Time.deltaTime;

    transform.Translate(moveAmount);
}
}
