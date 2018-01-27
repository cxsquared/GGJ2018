using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject CameraGoalLookAt;
	public GameObject CameraGoalPosition;
	public Vector3 CameraCurrentLookAt;

	public float MovementDividor;
	public float LookAtDividor;

	void FixedUpdate() {

		Vector3 diff = CameraGoalPosition.transform.position - this.transform.position;


	
			
			this.transform.position = Vector3.Lerp (CameraGoalPosition.transform.position, this.transform.position, 0.95f);


		// Camera Look At
		Vector3 movementA = CameraGoalLookAt.transform.position - CameraCurrentLookAt;
		movementA = movementA / LookAtDividor;
		CameraCurrentLookAt += movementA;
		transform.LookAt(CameraCurrentLookAt);




		// If you dont' want the camera to have a smooth panning / turning one, then just replace the camera look at code with:
		//  transform.lookt(CameraGoalLookAt);
	}

}
