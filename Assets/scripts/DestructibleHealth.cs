using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleHealth : MonoBehaviour {

	public int health = 3;
	private Animator animator;

    private List<string> TransmitorNames = new List<string>(new string[] { "transmitor", "relay", "blocking hub" });

	void Start() {
		animator = this.GetComponent<Animator> ();
	}

	public void AttackHit(object[] hitInfo) {

		this.health -= 1;

		if (this.health <= 0) {
			if (true) {
				DataGatherer.Instance.AddEvent (new GameEvent (GameEventEnum.DESTORY_TRANSMITOR, TransmitorNames.GetRandom()));
			}
			//set the kill trigger on the animator
			Destroy (this.gameObject);
		}
	}
}
