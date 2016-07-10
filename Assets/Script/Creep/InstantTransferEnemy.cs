using UnityEngine;
using System.Collections;

public class InstantTransferEnemy : MonoBehaviour {

	[HideInInspector]
	public bool instantTransferMode;

	bool isMidPoint1Reached;
	bool isMidPoint2Reached;
	bool isEndPointReached;
	float step;
	float intantTransferSpeed = 10f;
	Transform enemySpawnerSpot;
	Vector3 startPos;
	Vector3 midPos1;
	Vector3 midPos2;
	Vector3 endPos;
	bool init = true;

	// Use this for initialization
	void Start () {
		enemySpawnerSpot = GameObject.FindWithTag ("EnemySpawner").transform;
		step = intantTransferSpeed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (instantTransferMode) {
			// initialize, run only once each instant transfer cycle
			if (init) {
				startPos = transform.position;
				midPos1 = new Vector3 (transform.position.x, 10f, transform.position.z);
				midPos2 = new Vector3 (enemySpawnerSpot.position.x, 10f, enemySpawnerSpot.position.z);
				endPos = enemySpawnerSpot.position;
				init = false;
			}

			// check if certain path point is reached
			if (Vector3.Distance(transform.position, midPos1) <= 1f)
				isMidPoint1Reached = true;
			if (Vector3.Distance(transform.position, midPos2) <= 1f)
				isMidPoint2Reached = true;
			if (Vector3.Distance(transform.position, endPos) <= 1f)
				isEndPointReached = true;

			// move it: startPos -> midPos1 -> midPos2 -> endPos
			if (!isMidPoint1Reached) {
				transform.Find ("Trail").gameObject.GetComponent<TrailRenderer> ().enabled = true;
				transform.Translate((midPos1-startPos).normalized * step, Space.World);
			}
			if (isMidPoint1Reached && !isMidPoint2Reached) {
				transform.Translate((midPos2-midPos1).normalized * step, Space.World);
			}
			if (isMidPoint2Reached && !isEndPointReached) {
				transform.Translate ((endPos-midPos2).normalized * step, Space.World);
			}

			// reset variables if end point is reached
			if (isEndPointReached) {
				transform.Find ("Trail").gameObject.GetComponent<TrailRenderer> ().enabled = false;	
				// reset bool instantTransferMode
				instantTransferMode = false;
				init = true;
				// reset path marker
				isMidPoint1Reached = false;
				isMidPoint2Reached = false;
				isEndPointReached = false;
				// reset the path node
				transform.GetComponent<Enemy>().targetPathNode = NewMap.pathPoints [0].transform;
				transform.GetComponent<Enemy>().pathNodeIndex = 0;
			}
		}
	}
}
