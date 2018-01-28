using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject CameraGoalLookAt;
	public GameObject CameraGoalPosition;
	public Vector3 CameraCurrentLookAt;


	void Update() {

		Vector3 diff = CameraGoalPosition.transform.position - this.transform.position;
			
		this.transform.position = Vector3.Lerp (CameraGoalPosition.transform.position, this.transform.position, 0.95f);


		Vector3 pos = CameraGoalPosition.transform.position;
		Quaternion newRot = Quaternion.LookRotation(pos);
		//newRot.x = 
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.9f); 


		//this.transform.LookAt (CameraGoalLookAt.transform.position);

		//transform.LookAt(CameraCurrentLookAt);
		// If you dont' want the camera to have a smooth panning / turning one, then just replace the camera look at code with:
		//  transform.lookt(CameraGoalLookAt);
	}

}
