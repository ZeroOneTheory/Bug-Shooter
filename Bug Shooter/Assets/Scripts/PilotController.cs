using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Input.GetAxisRaw("Vertical"),0.0f, Input.GetAxisRaw("Horizontal"));
	}
}
