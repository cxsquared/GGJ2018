using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleHealth : MonoBehaviour {

	public int health = 3;
	private Animator animator;

	void Start() {
		animator = this.GetComponent<Animator> ();
	}

	public void AttackHit(object[] hitInfo) {

		Debug.Log ("Aaug");
		this.health -= 1;


		if (this.health <= 0) {
			//set the kill trigger on the animator
			Destroy (this.gameObject);
		}
	}
}
