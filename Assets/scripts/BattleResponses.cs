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
        SoundManager.Instance.PlayBigEnemyHit(transform.position);

		if (hp <= 0) {
			TriggerDeath ();
		}
	}

	void TriggerDeath() {
        SoundManager.Instance.PlayBigEnemyDeath(transform.position);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
