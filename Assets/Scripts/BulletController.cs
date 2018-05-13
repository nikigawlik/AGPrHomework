using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float speed = 1f;
	public float lifetime = 3f; // seconds

	private float aliveFor = 0f;

	public GameObject destroyEffect;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		transform.localPosition = transform.position + transform.forward * speed;

		aliveFor += Time.fixedDeltaTime;
		if (aliveFor > lifetime) {
			DestroyMe(false);
		}
	}


	private void OnCollisionEnter(Collision other) {
		DestroyMe(true);
	}

	private void DestroyMe(bool doEffect) {
		if(destroyEffect != null && doEffect) {
			GameObject.Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
		}
		GameObject.Destroy(gameObject);
	}
}
