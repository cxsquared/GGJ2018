using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

[RequireComponent(typeof(SphereCollider))]
public class DataPad : MonoBehaviour {

    bool HasTriggered = false;

    bool IsVisible = false;

    public Text textObj;
    public Text instructions;

    public string Entry;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsVisible && Input.GetKeyDown(KeyCode.E))
        {
            textObj.gameObject.SetActive(false);
            instructions.gameObject.SetActive(false);
        }	
	}

    private void  OnTriggerEnter(Collider other)
    {
        if (!HasTriggered && other.tag == "Player")
        {
            HasTriggered = true;
            IsVisible = true;
            textObj.text = Entry;
            textObj.gameObject.SetActive(true);
            instructions.gameObject.SetActive(true);
        }
    }
}
