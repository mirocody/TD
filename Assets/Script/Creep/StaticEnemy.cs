using UnityEngine;
using System.Collections;
using System;

public class StaticEnemy : Enemy {

	private float healthBarCoolDownTime;

	public GameObject towerspot;
	public GameObject firetower;
	public GameObject watertower;
	public GameObject earthtower;
	public GameObject woodtower;
	public GameObject metaltower;


	// Use this for initialization
	void Start () {
        InitialData initData = GameObject.Find("SystemData").GetComponent<InitialData>();
        if (level == 6)
        {
            max_enemyHealth = initData.enemyhealth[5];
            enemyGold = initData.enemygold[5];
            enemyScore = initData.enemyscore[5];
            enemySpeed = initData.enemyspeed[5];
            enemyElement = 's';
        }
        //health Bar
        this.max_enemyHealth = 100;
		healthBarCoolDownTime = 0;
		enemyHealth = this.max_enemyHealth;
		this.my_health = (GameObject)Instantiate(this.healthBar, this.transform.position, this.transform.rotation);
		//Update Health Bar Location.
		my_health.GetComponent<Transform>().position = new Vector3(
			Camera.main.WorldToScreenPoint(this.transform.position).x,
			Camera.main.WorldToScreenPoint(this.transform.position).y + 40f,
			0
		);
		my_health.SetActive (false);
		//SelectedArrow.
		my_Arrow = (GameObject)Instantiate(this.selectedArrow, this.transform.position, this.transform.rotation);
		my_Arrow.SetActive(false);

		//initial Arrow Location.
		my_Arrow.GetComponent<Transform>().position = new Vector3(
			Camera.main.WorldToScreenPoint(this.transform.position).x,
			Camera.main.WorldToScreenPoint(this.transform.position).y + 70f,
			0
		);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (healthBarCoolDownTime > 0) {
			my_health.SetActive (true);
			healthBarCoolDownTime -= 1*Time.deltaTime;
		} else {
			my_health.SetActive (false);
		}

	}

	// Set Health Bar
	public override void SetHealthBar(float calc_health)
	{
		my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1, 1);
	}

	public override void TakeDamage(float damage, char element) {
		healthBarCoolDownTime = 3;
		Debug.Log("Take Damage in Static");
		enemyHealth -= damage;

		float calc_health = enemyHealth / this.max_enemyHealth;
		Debug.Log ("calc" + this.max_enemyHealth);
		SetHealthBar(calc_health);

		if(enemyHealth <= 0) {
			DieInStatic ();
		}
	}

	public void DieInStatic() {
		// TODO: Do this more safely!
		GoldManager.gold += enemyGold; //+enemygold
		Destroy(gameObject);
		Destroy(my_health);
		Destroy (my_Arrow);
		if (TargetEnemyController.targetEnemey == this.transform) {
			TargetEnemyController.targetEnemey = null;
		}
		InstantiateNewGameObject ();
	}

	public void InstantiateNewGameObject() {
		
		Vector3 position = this.transform.position;
        position.y = 0.07f;
        Debug.Log ("Die In Instantiate");
        int choice = InitialData.randomTower;
		GameObject temp = new GameObject ();
		Debug.Log ("InstantiateNewGameObject" + choice);
		if (choice >= 97) {
			temp = (GameObject)Instantiate (firetower, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
		} else if (choice >= 94) {
			temp = (GameObject)Instantiate (watertower, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
		} else if (choice >= 91) {
			temp = (GameObject)Instantiate (earthtower, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
		} else if (choice >= 88) {
			temp = (GameObject)Instantiate (woodtower, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
		} else if (choice >= 85) {
			temp = (GameObject)Instantiate (metaltower, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
		} else {
			Debug.Log ("Instantiate of 95");
			temp = (GameObject)Instantiate (towerspot, position, this.transform.rotation);
			temp.transform.Rotate(0, 180, 0);
			Debug.Log ("Instantiate of 95!!");
		}

		temp.name += this.transform.name;
	}


}
