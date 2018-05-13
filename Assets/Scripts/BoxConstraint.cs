using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConstraint : MonoBehaviour {

	[Tooltip("limit of movement in the plus or minus direction, for each axis")]
	public Vector3 limits = Vector3.zero;
	[Tooltip("center of the box")]
	public Vector3 center = Vector3.zero;
	[Tooltip("stregth of the allpied force outside of bounds. 1 for instant")]
	public float stregth = 1f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		Vector3 correctedPos = Vector3.Min(Vector3.Max(transform.localPosition, center - limits), center + limits);
		rb.AddForce((correctedPos - transform.localPosition) * stregth, ForceMode.VelocityChange);
	}
}
