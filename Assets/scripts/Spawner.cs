using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject thingToSpawn;
	public Transform spawnPosition;
	public float interval;
	private float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time + interval;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= spawnTime) {
			//make enemy
			Instantiate(thingToSpawn, spawnPosition.position, Quaternion.identity);

			//increment time
			spawnTime = Time.time + interval;
		}
	}
}
