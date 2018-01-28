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


		hp -= (int) hitInfo[0];

		if (hp <= 0) {
			TriggerDeath ();
		} else {
			SoundManager.Instance.PlayBigEnemyHit(transform.position);
			animator.SetTrigger ("stagger");
			rb.isKinematic = false;
			rb.AddForce (( Vector3) hitInfo [1], ForceMode.Impulse);
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
