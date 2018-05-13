using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public Vector3 destructionGravity = Vector3.down;
	public float explosionsPerSecond = 15;
	public GameObject explosionPrefab;
	public Bounds explosionZone;
	public int pointsAwarded = 100;

	private Vector3 motion = Vector3.zero;
	private bool isDestroyed = false;
	private float explosionDept = 0;

	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isDestroyed) {
			motion += destructionGravity * Time.fixedDeltaTime;
			transform.position += motion * Time.fixedDeltaTime;

			explosionDept += explosionsPerSecond * Time.fixedDeltaTime;
			while(explosionDept >= 1f) {
				explosionDept -= 1f;
				Vector3 explosionPos =new Vector3(Random.Range(-1f, 1f), 
					Random.Range(-1f, 1f), 
					Random.Range(-1f, 1f)
				);
				explosionPos.Scale(explosionZone.extents);
				Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z) 
					+ explosionZone.center + explosionPos;
				GameObject.Instantiate(explosionPrefab, spawnPos, Random.rotation);
			}
		}
	}

	private void OnCollisionEnter(Collision other) {
		// Debug.Log("BAAM");
		if(!isDestroyed) {
			// do points
			gc.points += pointsAwarded;
			isDestroyed = true;
		}
	}
}
