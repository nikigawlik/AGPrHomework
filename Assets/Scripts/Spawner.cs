using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float minDelay = 1f;
	public float maxDelay = 2f;
	public float initialDelay = 3f;
	public bool useSpawnerRotation = true;

	public GameObject[] prefabs;

	private float countdown;
	private BoxCollider bc;

	// Use this for initialization
	void Start () {
		countdown = initialDelay;
		bc = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		Bounds bounds = bc.bounds;
		countdown -= Time.deltaTime;
		if (countdown <= 0) {
			// spawn object
			Vector3 pos = new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				Random.Range(bounds.min.y, bounds.max.y),
				Random.Range(bounds.min.z, bounds.max.z)
			);

			GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

			Instantiate(prefab, pos, useSpawnerRotation? transform.rotation : prefab.transform.rotation);

			// reset timer
			countdown = Random.Range(minDelay, maxDelay);
		}
	}
}
