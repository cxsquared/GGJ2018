using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int totalHealth;
	public int regenDelay;
	private float nextRegen;
	private int _currentHealth;

	// Use this for initialization
	void Start () {
		
	}

	public void damage(int damageAmount) {
		_currentHealth -= damageAmount;

		if (_currentHealth < 0)
			_currentHealth = 0;

		nextRegen = Time.time + regenDelay;
	}

	// Update is called once per frame
	void Update () {
		if(Time.time >= nextRegen) {
			_currentHealth = Mathf.Clamp (_currentHealth + 1, 0, totalHealth);
			nextRegen = Time.time + regenDelay;
		}
	}
}
