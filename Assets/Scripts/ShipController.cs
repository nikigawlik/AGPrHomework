using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	[Tooltip("sideways speed of the spacecraft")]
	public float strafeForce = 100f;
	[Tooltip("for/backwards speed of the spacecraft")]
	public float moveForce = 100f;
	[Tooltip("the game object that represents the ship visually")]
	public GameObject displayObj;

	[Header("Display")]
	[Tooltip("rotation of the spacecraft while moving")]
	public float strafeRotation = 1f;

	public float collisionBounce = 1000f;
	public float jitter = .5f;

	public GameObject explosionPrefab;

	private Vector3[] trail;

	private Rigidbody rb;
	private LineRenderer lr;
	private GameController gc;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		float rotationStrafe = -rb.velocity.x * strafeRotation;
		displayObj.transform.localRotation = Quaternion.Euler(0, 0, rotationStrafe);
	}

	void FixedUpdate() {
		float strafe = Input.GetAxis("Horizontal");
		float lift = Input.GetAxis("Vertical");

		rb.AddForce(transform.right * strafe * strafeForce);
		rb.AddForce(transform.forward * lift * moveForce);
	}

	private void OnCollisionEnter(Collision other) {
		Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
		GameObject.Destroy(gameObject);
		gc.scroll = new Vector3(0, 0, 0);
		gc.gameOverScreen.SetActive(true);
	}
}
