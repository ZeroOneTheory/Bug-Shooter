﻿using System.Collections;
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

    // Gets the input axis from the controller or keyboard
    Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    // The normalized vector of these two inputs, equals the direction the player is pressing on a joystick or from a WASD keyboard
    Vector3 direction = input.normalized;

    // Velocity is the player speed times the dirction it's facing
    Vector3 velocity = speed * direction;
    // Move-Amount is equal to the velocity times the framerate of the game
    Vector3 moveAmount = velocity * Time.deltaTime;
    
    // Applys move amount to the players position
    transform.Translate(moveAmount);
}
}
