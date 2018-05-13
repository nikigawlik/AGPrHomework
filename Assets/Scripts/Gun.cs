using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	[Tooltip("the bullet game object prefab")]
	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Input
		if(Input.GetButtonDown("Fire1")) {
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
	}
}
