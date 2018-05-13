using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		// pass the collision to the parent
        transform.parent.gameObject.SendMessage("OnCollisionEnter", other);
	}
}

