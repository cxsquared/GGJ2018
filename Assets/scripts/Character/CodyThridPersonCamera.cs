using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyThridPersonCamera : MonoBehaviour {

    [SerializeField]
    private GameObject FollowCharacter;
    [SerializeField]
    private float DistanceUp = 3;
    [SerializeField]
    private float DistanceAway = 5;
    [SerializeField]
    private float Dampening = .5f;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        var targetPostion = FollowCharacter.transform.position + (FollowCharacter.transform.forward * -DistanceAway);
        targetPostion.y += DistanceUp;
        transform.position = Vector3.Lerp(transform.position, targetPostion, Time.deltaTime * Dampening);

        transform.LookAt(FollowCharacter.transform);
	}
}
