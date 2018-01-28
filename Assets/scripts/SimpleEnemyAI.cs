﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleEnemyAI : MonoBehaviour {

	public Transform goal;
	public float attackDistance;
	private UnityEngine.AI.NavMeshAgent agent;
	private GameObject playerRef;
	private Animator animator;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = this.gameObject.GetComponent<Animator> ();
		playerRef = GameObject.FindGameObjectWithTag ("Player");
	}
		
	void Update() {
		agent.destination = playerRef.transform.position;

		if(agent.remainingDistance < 3f && !agent.pathPending) {
			animator.SetTrigger("attack");
		}

	}
}