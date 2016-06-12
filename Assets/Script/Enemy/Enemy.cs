using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float enemyHealth;
	private int enemyGold;
	private int enemyScore;
	private float enemySpeed;
	private char enemyElement;
	public int level;

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	float pathNodeOffset = 0.2f;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find("Path");
		if(level==1){
			enemyHealth=10f;
			enemyGold=5;
			enemyScore=3;
			enemySpeed=6f;
			enemyElement='g';
		}
		if(level==2){
			enemyHealth=20f;
			enemyGold=10;
			enemyScore=6;
			enemySpeed=6f;
			enemyElement='m';
		}
		if(level==3){
			enemyHealth=30f;
			enemyGold=20;
			enemyScore=12;
			enemySpeed=6f;
			enemyElement='e';
		}
		if(level==4){
			enemyHealth=40f;
			enemyGold=40;
			enemyScore=24;
			enemySpeed=6f;
			enemyElement='w';
		}
		if(level==5){
			enemyHealth=50;
			enemyGold=50;
			enemyScore=36;
			enemySpeed=6f;
			enemyElement='f';
		}
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

		float distThisFrame = enemySpeed * Time.deltaTime;

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

	public void TakeDamage(float damage, char element) {
	//gold enemy
		if(enemyElement=='g'){
			if(element=='f'){
				damage=2*damage;
			}
			else if(element=='m'){
				damage=damage/2;
			}
		}
	//wood enemy
		if(enemyElement=='m'){
			if(element=='g'){
				damage=2*damage;
			}
			else if(element=='e'){
				damage=damage/2;
			}
		}
	//earth enemy
		if(enemyElement=='e'){
			if(element=='m'){
				damage=2*damage;
			}
			else if(element=='w'){
				damage=damage/2;
			}
		}
	//water enemy
		if(enemyElement=='w'){
			if(element=='e'){
				damage=2*damage;
			}
			else if(element=='f'){
				damage=damage/2;
			}
		}
		//fire enemy
		if(enemyElement=='f'){
			if(element=='w'){
				damage=2*damage;
			}
			else if(element=='g'){
				damage=damage/2;
			}
		}

		enemyHealth -= damage;
		if(enemyHealth <= 0) {
			Die();
		}
	}

	public void Die() {
		// TODO: Do this more safely!
		ScoreManager.score += enemyScore; //+enemyscore
		GoldManager.gold += enemyGold; //+enemygold
    SpawnerManager.stillAlive--;
		Destroy(gameObject);
	}
}
