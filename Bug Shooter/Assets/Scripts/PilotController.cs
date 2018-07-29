using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour {

    public float moveSpeed = 20.0f;
    public float enginePower = 50.0f;
    Vector3 levelZ;


    // Use this for initialization
    void Start () {
         
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveCamTo = transform.position - transform.forward * 10.0f + transform.up * 5.0f;
        Camera.main.transform.position = moveCamTo;
        Camera.main.transform.LookAt(transform.position);



        transform.position += transform.forward * Time.deltaTime * moveSpeed;
        moveSpeed -= transform.forward.y* enginePower* Time.deltaTime;
        if(moveSpeed < 5)
        {
            moveSpeed = 5;
        }

        transform.Rotate(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"), 0.0f);
        Debug.Log(Input.GetAxisRaw("Vertical"));
        Debug.Log(Input.GetAxisRaw("Horizontal"));




        float terrainHeightPosition = Terrain.activeTerrain.SampleHeight(transform.position);
        
        if(terrainHeightPosition > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                                             terrainHeightPosition,
                                             transform.position.z);
        }
	}
}
