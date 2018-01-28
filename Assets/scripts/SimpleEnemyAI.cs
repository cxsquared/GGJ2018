using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SimpleEnemyAI : MonoBehaviour {

	public Transform goal;
	public float attackDistance;
	private UnityEngine.AI.NavMeshAgent agent;
	private GameObject playerRef;
	private Animator animator;
	private Rigidbody rb;

	void Start () {
		

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.Warp (this.transform.position);

		animator = this.gameObject.GetComponent<Animator> ();
		playerRef = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody> ();


	}
		
	void Update() {
		//if (rb.velocity.magnitude < 0.01f)
		//	rb.isKinematic = true;
		//if (rb.isKinematic) {
			agent.destination = playerRef.transform.position;
		//}

		if(agent.remainingDistance < 3f && !agent.pathPending) {
			animator.SetTrigger("attack");
		}

	}
}
