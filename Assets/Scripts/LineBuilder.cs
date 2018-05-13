using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBuilder : MonoBehaviour {
	public Vector3 offsets = new Vector3(1, 0, 0);
	public GameObject[] prefabs;
	public int prespawn = 3;
	public int prespawnAxis = 0;
	public float prespawnFactor = 1;
	public Transform containingTransform;

	public int bufferLength = 8;
	private int bufferPointer = 0;
	private GameObject[] buffer;

	private Vector3 lastSpawnPos;

	// Use this for initialization
	void Start () {
		buffer = new GameObject[bufferLength];

		lastSpawnPos = transform.position - GetOffset(prespawnAxis, prespawnFactor);

		for(int i = 0; i < prespawn; i++) {
			if (offsets[prespawnAxis] >= 0) {
				SpawnPrefab(prespawnAxis, prespawnFactor);
				transform.position = lastSpawnPos;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 3; i++) {
			if (offsets[i] <= 0) 
				continue;
			if (Mathf.Abs((transform.position - lastSpawnPos)[i]) > offsets[i]) {
				SpawnPrefab(i, Mathf.Sign((transform.position - lastSpawnPos)[i]));
			}
		}
	}

	void SpawnPrefab(int axis, float factor) {
		// spawn prefab
		GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
		Vector3 curOffset = GetOffset(axis, factor);
		GameObject instance = prefab == null? null : Instantiate(prefab, lastSpawnPos + curOffset, prefab.transform.rotation);
		if(containingTransform != null && instance != null) {
			instance.transform.SetParent(containingTransform);
		}

		lastSpawnPos = lastSpawnPos + curOffset;

		if(buffer[bufferPointer] != null) {
			GameObject.Destroy(buffer[bufferPointer]);
		}
		buffer[bufferPointer] = instance;
		bufferPointer = (bufferPointer + 1) % bufferLength;
	}

	Vector3 GetOffset(int axis, float factor) {
		return new Vector3(axis==0? offsets[0]:0, axis==1? offsets[1]:0, axis==2? offsets[2]:0) * factor;
	}
}
