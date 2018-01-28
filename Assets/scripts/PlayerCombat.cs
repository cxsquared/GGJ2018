﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	private PlayerMove pMove;
	private Animator animator;
	private PlayerStats pStats;
	public float dodgeImpulse = 2.0f;


	// Use this for initialization
	void Start () {
		pMove = this.GetComponent<PlayerMove> ();
		pStats = this.GetComponent<PlayerStats> ();
		animator = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		//light
		if (Input.GetButtonDown ("Fire1")) {
			pMove.addImpact (this.gameObject.transform.forward, 4f);
			animator.SetTrigger ("lightAttack");
		}

		//heavy
		if (Input.GetButtonDown ("Fire2")) {
			pMove.addImpact (this.gameObject.transform.forward, 7f);
			animator.SetTrigger ("heavyAttack");
		}

		//Dodge
		if (Input.GetButtonDown ("Jump")) {
			pMove.addImpact ( new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")), dodgeImpulse );
			animator.SetTrigger ("dodge");
		}
	}

	public void AttackHit(object[] hitInfo) {
		pMove.addImpact (( Vector3) hitInfo [1], 20f);
		pStats.damage( (int) hitInfo[0] );
	}
}
