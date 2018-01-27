using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResponses : MonoBehaviour {

	private Rigidbody rb;
	public int hp;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}

	public void AttackHit(object[] hitInfo) {
		rb.AddForce (( Vector3) hitInfo [1]);
		hp -= (int) hitInfo[0];

		if (hp <= 0) {
			TriggerDeath ();
		}
	}

	void TriggerDeath() {
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
