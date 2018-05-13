using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantScroll : MonoBehaviour {
	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void FixedUpdate () {
		transform.position = transform.position + gc.scroll * Time.fixedDeltaTime;
	}
}
