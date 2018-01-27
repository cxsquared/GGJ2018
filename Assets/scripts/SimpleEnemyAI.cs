using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleEnemyAI : MonoBehaviour {

	public Transform goal;
	private UnityEngine.AI.NavMeshAgent agent;
	private GameObject playerRef;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		playerRef = GameObject.FindGameObjectWithTag ("Player");
	}
		
	void Update() {
		agent.destination = playerRef.transform.position;
	}
}
