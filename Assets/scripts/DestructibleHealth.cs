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

		this.health -= 1;

		if (this.health <= 0) {
			if (true) {
				DataGatherer.Instance.AddEvent (new GameEvent (GameEventEnum.DESTORY_TRANSMITOR));
			}
			//set the kill trigger on the animator
			Destroy (this.gameObject);
		}
	}
}
