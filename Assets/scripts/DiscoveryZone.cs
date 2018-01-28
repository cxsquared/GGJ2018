using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryZone : MonoBehaviour {

	public GameEventEnum objectType;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			DataGatherer.Instance.AddEvent (new GameEvent (objectType));
		}
	}

}
