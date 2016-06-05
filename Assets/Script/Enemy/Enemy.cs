using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float enemyhealth = 1f;
	public int enemygold = 10;
	public int enemyscore = 3;
	public float enemyspeed = 2.5f;

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	float pathNodeOffset = 2.0f;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find("Path");
	}

	void GetNextPathNode() {
		if(pathNodeIndex < pathGO.transform.childCount) {
			targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
			pathNodeIndex++;
		}
		else {
			targetPathNode = null;
			//ReachedGoal();
		}
	}

	// Update is called once per frame
	void Update () {
		if(targetPathNode == null) {
			GetNextPathNode();
			if(targetPathNode == null) {
				// We've run out of path!
				ReachedGoal();
				return;
			}
		}

		Vector3 offset = new Vector3(Random.Range(-pathNodeOffset, pathNodeOffset), 0, Random.Range(-pathNodeOffset, pathNodeOffset));
		Vector3 dir = targetPathNode.position - this.transform.localPosition + offset;

		float distThisFrame = enemyspeed * Time.deltaTime;

		if(dir.magnitude <= distThisFrame + pathNodeOffset) {
			// We reached the node
			targetPathNode = null;
		}
		else {
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			Quaternion targetRotation = Quaternion.LookRotation( dir );
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*Random.Range(5,10));
		}

	}

	void ReachedGoal() {
		GameObject.FindObjectOfType<ScoreManager>().LoseLife();
        SpawnerManager.stillAlive--;
		Destroy(gameObject);
	}

	public void TakeDamage(float damage) {
		enemyhealth -= damage;
		if(enemyhealth <= 0) {
			Die();
		}
	}

	public void Die() {
		// TODO: Do this more safely!
		ScoreManager.score += 1; //+enemyscore
		GoldManager.gold += 3; //+enemygold
        SpawnerManager.stillAlive--;
		Destroy(gameObject);
	}
}
