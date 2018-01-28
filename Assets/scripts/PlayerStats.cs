using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int totalHealth;
	public int regenDelay;
	private Animator animator;
	private float nextRegen;
	private int _currentHealth;
	private Transform startPoint;
	private bool ded = false;

	// Use this for initialization
	void Start () {
		startPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;
		animator = this.GetComponent<Animator> ();
	}

	public void damage(int damageAmount) {

		_currentHealth -= damageAmount;

		if (_currentHealth < 0 && !ded) {
			_currentHealth = 0;

			//trigger game over / reset
			DataGatherer.Instance.AddEvent (new GameEvent (GameEventEnum.DEATH_BASIC));

			animator.SetTrigger ("kill");
		} else {
			animator.SetTrigger ("stagger");
		}

		nextRegen = Time.time + regenDelay;
	}

	void Respawn() {
		_currentHealth = totalHealth;
		ded = false;

		animator.SetTrigger ("respawn");

		this.transform.position = this.startPoint.position;
		this.transform.rotation = this.startPoint.rotation;
	}

	// Update is called once per frame
	void Update () {
		if(Time.time >= nextRegen) {
			_currentHealth = Mathf.Clamp (_currentHealth + 1, 0, totalHealth);
			nextRegen = Time.time + regenDelay;
		}
	}
}
