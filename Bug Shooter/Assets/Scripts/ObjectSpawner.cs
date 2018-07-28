using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject rockPrefabs;

    Vector3 screenHalfSize;
    public float secondsBetweenSpawn = 1;
    public float spawnLayer = -25;
    float nextSpawnTime;


	// Use this for initialization
	void Start () {
        screenHalfSize = new Vector3(Camera.main.aspect * Camera.main.orthographicSize,Camera.main.orthographicSize,0);

    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawn;
            Vector3 spawnPosition = new Vector3(Random.Range(-screenHalfSize.x, screenHalfSize.x), spawnLayer, screenHalfSize.y);
            Instantiate(rockPrefabs, spawnPosition, Quaternion.identity);

        }
	}
}
