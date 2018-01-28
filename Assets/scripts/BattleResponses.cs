using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResponses : MonoBehaviour {

	private Rigidbody rb;
	private Animator animator;
	public int hp;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		animator = this.GetComponent<Animator> ();
	}

	public void AttackHit(object[] hitInfo) {
		rb.AddForce (( Vector3) hitInfo [1]);
		hp -= (int) hitInfo[0];

        SoundManager.Instance.PlayBigEnemyHit(transform.position);
		animator.SetTrigger ("stagger");

		if (hp <= 0) {
			TriggerDeath ();
		}
	}

	void TriggerDeath() {
        SoundManager.Instance.PlayBigEnemyDeath(transform.position);
		animator.SetTrigger ("kill");
	}

	public void FinishDeath() {
		Destroy (this.gameObject);
	}

}
