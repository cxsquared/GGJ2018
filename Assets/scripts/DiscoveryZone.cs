using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryZone : MonoBehaviour {

	public GameEventEnum objectType;

    bool IsSent = false;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			DataGatherer.Instance.AddEvent (new GameEvent (objectType));
            if (objectType == GameEventEnum.DISCOVER_RADIO_TOWER && !IsSent)
            {
                DataGatherer.Instance.SendTweet(1);
                IsSent = true;
            }
		}
	}

}
