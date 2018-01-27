using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public float attackForce = 160f;
	private BoxCollider bc;
	private bool enableCheck = false;

	void Awake() {
		bc = this.GetComponent<BoxCollider> ();
	}


	void OnEnable() {


		Collider[] hits = Physics.OverlapBox (bc.center, bc.size); //maybe?

		foreach (Collider hit in hits) {
			CheckAttack (hit);
		}
		//enableCheck = false;
	}
	/*
	void OnTriggerStay(Collider other) {
		if (!enableCheck) {
			CheckAttack (other);
		}
	}

	void Update() {
		enableCheck = true;
	}
	*/

	void OnTriggerEnter(Collider other) {
		CheckAttack (other);
	}

	void CheckAttack(Collider other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			//talk to the enemy and tell it its "pain time"

			object[] eventInfo = new object[2];
			eventInfo[0] = 1;
			eventInfo [1] = this.gameObject.transform.forward * attackForce;

			other.gameObject.SendMessage("AttackHit", eventInfo, SendMessageOptions.DontRequireReceiver);
		}
	}

}
