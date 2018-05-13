using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	[Header("Setup")]
	public BoxCollider enemyArea;
	public GameObject playerObject;
	public GameObject gameOverScreen;
	
	[Header("Settings")]
	public Vector3 scroll;

	[Header("Game Variables")]
	public int points;

	// Use this for initialization
	void Start () {
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
