using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float enemyHealth;		//cur health
	private float max_enemyHealth;	//max Health
	private int enemyGold;
	private int enemyScore;
	private float enemySpeed;
	private char enemyElement;
	public int level;
	public GameObject healthBar;	//healthBar
	private GameObject my_health;	//initiated healthBar
	public Transform coin;			//coin
	public GameObject coin_out;		//a place where coin comes out

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	float pathNodeOffset = 0.2f;

	// Use this for initialization
	void Start () {
		//pathGO = NewMap.pathPoints[pathNodeIndex];

		if(level==1){
			max_enemyHealth=10f;		//modify to be max instead of w/o max
			enemyGold=5;
			enemyScore=3;
			enemySpeed=1f/*6f*/;
			enemyElement='g';
		}
		if(level==2){
			max_enemyHealth=20f;
			enemyGold=10;
			enemyScore=6;
			enemySpeed=6f;
			enemyElement='m';
		}
		if(level==3){
			max_enemyHealth=30f;
			enemyGold=20;
			enemyScore=12;
			enemySpeed=6f;
			enemyElement='e';
		}
		if(level==4){
			max_enemyHealth=40f;
			enemyGold=40;
			enemyScore=24;
			enemySpeed=6f;
			enemyElement='w';
		}
		if(level==5){
			max_enemyHealth=50f;
			enemyGold=50;
			enemyScore=36;
			enemySpeed=6f;
			enemyElement='f';
		}

		//Health Bar
		enemyHealth = max_enemyHealth;
		my_health = (GameObject)Instantiate (healthBar, this.transform.position, this.transform.rotation);

	}

	void GetNextPathNode() {
		if(pathNodeIndex < 11) {
			targetPathNode = NewMap.pathPoints[pathNodeIndex].transform;
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

		// Update Health Bar Location
		my_health.GetComponent<Transform>().position = new Vector3(
			Camera.main.WorldToScreenPoint(this.transform.position).x,
			Camera.main.WorldToScreenPoint(this.transform.position).y + 75f,
			0
		);
	}

	// Set Health Bar
	public void SetHealthBar(float calc_health) {
		//enemyHealth value 0-1
		Debug.Log("calc_health * healthBar.transform.localScale.x" + calc_health * healthBar.transform.localScale.x);
		my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1,  1);

	}

	void ReachedGoal() {
		GameObject.FindObjectOfType<HPManager>().LoseLife();
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
		// calculate health bar and set
		float calc_health = enemyHealth / max_enemyHealth;
		SetHealthBar (calc_health);

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
		// instantiate a coin
		Instantiate(coin, coin_out.transform.position, Quaternion.identity);

	}
}
