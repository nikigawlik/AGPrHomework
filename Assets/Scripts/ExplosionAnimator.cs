using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour {

	public float animDuration = 1;
	private float startScale = 1;
	private float endScale = 2;

	private float time;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float progress = time/animDuration;
		
		Material mat = GetComponent<MeshRenderer>().material;
		mat.SetFloat("_AnimationProgress", progress);

		float scale = startScale + progress * (endScale - startScale);
		transform.localScale = new Vector3(scale, scale, scale);
		
		time = time + Time.deltaTime;
		
		if(time > animDuration) {
			GameObject.Destroy(gameObject);
		}
	}
}
