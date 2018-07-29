using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float bulletForce = 4000.0f;

    private void Awake()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce);
    }
    private void FixedUpdate()
    {
        BulletLife();
    }

    //public GameObject bulletHolePrefab;
    public float distanceFromWall = 0.15f;
    void BulletLife()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000.0f))
        {
            if(hit.distance < 3)
            {
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 4.0f);
    }
}
