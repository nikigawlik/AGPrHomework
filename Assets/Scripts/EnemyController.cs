using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed = 1f;

	enum State {
		APPROACH,
		REST
	}

	private Vector3 targetPos;
	private State state = State.APPROACH;
	private GameController gc;
	private BoxCollider targetArea;
	private GameObject finalTarget;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		targetArea = gc.enemyArea;
		finalTarget = gc.playerObject;

		Bounds bounds = targetArea.bounds;
		targetPos = new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				Random.Range(bounds.min.y, bounds.max.y),
				Random.Range(bounds.min.z, bounds.max.z)
		);
	}
	
	void FixedUpdate () {
		// turn towards target
		float angle = -Mathf.Atan2(targetPos.z - transform.position.z, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, angle, 0);
		switch (state) {
			case State.APPROACH:
				// move towards target
				transform.position = transform.position + (targetPos - transform.position).normalized * speed;

				if ((targetPos - transform.position).magnitude < speed) {
					state = State.REST;
					targetPos = finalTarget.transform.position;
				}
			break;
			case State.REST:
				// pass
			break;
		}
	}
}
