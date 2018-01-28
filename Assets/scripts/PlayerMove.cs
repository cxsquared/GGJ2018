using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed;
	public float gravity = 20.0F;
	private CharacterController cController;
	private float mass = 3.0f; // defines the character mass
	private Vector3 impact = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	private Transform camTrans;


	// Use this for initialization
	void Start () {
		this.cController = this.GetComponent<CharacterController> ();

		camTrans = Camera.main.transform;
	}

	public void addImpact(Vector3 dir, float force){
		dir.Normalize ();
		if (dir.y < 0)
			dir.y = -dir.y; // reflect down force on the ground
		impact += dir.normalized * force / mass;
	}

	// Update is called once per frame
	void Update () {

		// 
		if (this.impact.magnitude > 0.2) cController.Move(impact * Time.deltaTime);

		// consumes the impact energy each cycle
		impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);

		//
		//moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		//moveDirection = transform.TransformDirection(moveDirection);

		Vector3 turnDirection = new Vector3 (0f, Input.GetAxis ("Horizontal") * 90f, 0f);

		this.transform.rotation = Quaternion.Euler (turnDirection);

		moveDirection = this.transform.forward;
		moveDirection *= speed;

		moveDirection.y -= gravity * Time.deltaTime;
		cController.Move(moveDirection * Time.deltaTime);
	}
}
