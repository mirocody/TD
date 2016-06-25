using UnityEngine;
using System.Collections;

public class StaticEnemy : Enemy {

	private int healthBarCoolDownTime;
	private GameObject my_health;
	private float enemyHealth;

	public GameObject towerspot;


	// Use this for initialization
	void Start () {
		//health Bar
		this.max_enemyHealth = 100;
		healthBarCoolDownTime = 0;
		enemyHealth = this.max_enemyHealth;
		my_health = (GameObject)Instantiate(this.healthBar, this.transform.position, this.transform.rotation);
		//Update Health Bar Location.
		my_health.GetComponent<Transform>().position = new Vector3(
			Camera.main.WorldToScreenPoint(this.transform.position).x,
			Camera.main.WorldToScreenPoint(this.transform.position).y+40f,
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
		}

	}

	// Set Health Bar
	public override void SetHealthBar(float calc_health)
	{
		my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1, 1);
	}

	public override void TakeDamage(float damage, char element) {

		enemyHealth -= damage;
		healthBarCoolDownTime = 1;

		float calc_health = enemyHealth / this.max_enemyHealth;
		Debug.Log ("calc" + this.max_enemyHealth);
		SetHealthBar(calc_health);

		if(enemyHealth <= 0) {
			Die('s');
		}
	}

	public override void Die(char element) {
		// TODO: Do this more safely!
		GoldManager.gold += enemyGold; //+enemygold
		Destroy(gameObject);
		Destroy(my_health);
		Destroy (my_Arrow);
		if (TargetEnemyController.targetEnemey == this.transform) {
			TargetEnemyController.targetEnemey = null;
		}
		Vector3 position = this.transform.position;
		GameObject temp = (GameObject)Instantiate (towerspot, position, this.transform.rotation);
		temp.name += this.transform.name;
	}


}
