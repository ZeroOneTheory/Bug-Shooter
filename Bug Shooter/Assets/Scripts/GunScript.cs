using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Shooting();
	}

    public Transform canonRight;
    public Transform canonLeft;
    public GameObject bulletPrefab;
    public float fireRate = 6.0f;
    private float waitTilFire = 0.0f;
    void Shooting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (waitTilFire <= 0)
            {
                Instantiate(bulletPrefab, canonRight.transform.position, canonRight.transform.rotation);
                Instantiate(bulletPrefab, canonLeft.transform.position, canonLeft.transform.rotation);
                waitTilFire = 1;

            }
        }
        waitTilFire -= fireRate * Time.deltaTime;
    }
}
