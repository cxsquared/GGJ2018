using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

	public GameObject CameraGoalLookAt;
	public GameObject CameraGoalPosition;
	public Vector3 CameraCurrentLookAt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Camera Look At
		this.gameObject.transform.LookAt(CameraGoalLookAt.transform);


	//	Vector3 pos = CameraGoalPosition.transform.position;
	//	Quaternion newRot = Quaternion.LookRotation(pos);
		//newRot.x = 
	//	transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.9f); 
	}
}
