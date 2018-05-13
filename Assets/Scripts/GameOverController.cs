using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	public float delayBeforeRestart = 1f;

	private float timer;

	// Use this for initialization
	void Start () {
		timer = delayBeforeRestart;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		else if(Input.anyKeyDown) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
