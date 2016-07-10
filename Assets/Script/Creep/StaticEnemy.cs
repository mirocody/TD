using UnityEngine;
using System.Collections;
using System;

public class StaticEnemy : Enemy {

	private int healthBarCoolDownTime;
	//private GameObject my_health;
	//private float enemyHealth;

	public GameObject towerspot;
	public GameObject firetower;
	public GameObject watertower;
	public GameObject earthtower;
	public GameObject woodtower;
	public GameObject metaltower;
    public GameObject fireCard;
    public GameObject woodCard;
    public GameObject earthCard;
    public GameObject waterCard;
    public GameObject metalCard;
    private float gap1;
    private float gap2;
	bool isDead;


	// Use this for initialization
	void Start () {
		isDead = false;
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

        //Caculate gap value for every element card and tower.
        gap1 = (float)(InitialData.towerProbability - InitialData.elementProbability) / 5;
        gap2 = (float)(InitialData.elementProbability - InitialData.spotProbability) / 5;
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

	public override void TakeDamage(float damage, char towerElement,int level,bool stunFlag) {
        Debug.Log("Take Damage in Static");
		enemyHealth -= damage;
		healthBarCoolDownTime = 1;

		float calc_health = enemyHealth / this.max_enemyHealth;
		Debug.Log ("calc" + this.max_enemyHealth);
		SetHealthBar(calc_health);

		if(enemyHealth <= 0&&!isDead) {
			isDead = true;
			DieInStatic ();
		}
	}

	public void DieInStatic() {
		// TODO: Do this more safely!
		GoldManager.gold += enemyGold; //+enemygold
		Debug.Log ("Diedie");
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
		int choice = UnityEngine.Random.Range (0, 100);
        //int choice = 80;
		GameObject temp = new GameObject ();
		Debug.Log ("InstantiateNewGameObject" + choice);
		if (choice >= InitialData.elementProbability + 4*gap1) {
			temp = (GameObject)Instantiate (firetower, position, this.transform.rotation);
		} else if (choice >= InitialData.elementProbability + 3*gap1) {
			temp = (GameObject)Instantiate (watertower, position, this.transform.rotation);
		} else if (choice >= InitialData.elementProbability + 2*gap1) {
			temp = (GameObject)Instantiate (earthtower, position, this.transform.rotation);
		} else if (choice >= InitialData.elementProbability+gap1) {
			temp = (GameObject)Instantiate (woodtower, position, this.transform.rotation);
		} else if (choice >= InitialData.elementProbability) {
			temp = (GameObject)Instantiate (metaltower, position, this.transform.rotation);
		} else if (choice >= InitialData.spotProbability)
        {
            dropElementCard(temp,position,choice);
        } else {
			temp = (GameObject)Instantiate (towerspot, position, this.transform.rotation);
		}

		temp.name += this.transform.name;
	}

    void dropElementCard(GameObject temp, Vector3 position,int choice)
    {
        Vector3 tempPostion = new Vector3(position.x, position.y+0.5f, position.z);
        Quaternion tempRotation = new Quaternion(0, 120, 180, 0);
        if (choice >= InitialData.spotProbability + 4 * gap2)
        {
            temp = (GameObject)Instantiate(fireCard, tempPostion, tempRotation);
        }
        else if(choice>= InitialData.spotProbability + 3 * gap2)
        {
            temp = (GameObject)Instantiate(woodCard, tempPostion, tempRotation);
        }
        else if(choice>= InitialData.spotProbability + 2 * gap2)
        {
            temp = (GameObject)Instantiate(waterCard, tempPostion, tempRotation);
        }
        else if (choice >= InitialData.spotProbability + gap2)
        {
            temp = (GameObject)Instantiate(earthCard, tempPostion, tempRotation);
        }
        else
        {
            temp = (GameObject)Instantiate(metalCard, tempPostion, tempRotation);
        }
        GameObject temp1 = (GameObject)Instantiate(towerspot, position, this.transform.rotation);
        temp1.name += this.transform.name;
    }

}
