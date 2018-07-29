using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform follow;

    void Awake()
    {
        follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Vector3 velocityCameraFollow;
    public Vector3 followPosition = new Vector3(0, 2, -4);
    public float angle;
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, follow.transform.TransformPoint(followPosition) + Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, 0.1f);
        transform.rotation = Quaternion.Euler(new Vector3(angle, follow.GetComponent<DroneController>().currentYRotation, 0.0f));
    }
}
