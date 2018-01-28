using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	private Rigidbody rb;
	private Animator animator;
	private PlayerStats pStats;
	public float dodgeImpulse = 2.0f;


	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		pStats = this.GetComponent<PlayerStats> ();
		animator = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		//light
		if (Input.GetButtonDown ("Fire1")) {
			//rb.AddForce(this.gameObject.transform.forward * 100f);
			animator.SetTrigger ("lightAttack");
		}

		//heavy
		if (Input.GetButtonDown ("Fire2")) {
			//rb.AddForce (this.gameObject.transform.forward * 100f);
			animator.SetTrigger ("heavyAttack");
		}

		//Dodge
		if (Input.GetButtonDown ("Dodge")) {
			//rb.AddForce ( this.gameObject.transform.forward * dodgeImpulse );
			animator.SetTrigger ("dodge");
		}
	}

	public void AttackHit(object[] hitInfo) {
		//rb.AddForce (( Vector3) hitInfo [1], ForceMode.Impulse);
		pStats.damage( (int) hitInfo[0] );
	}
}
