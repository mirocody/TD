<<<<<<< HEAD
using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour {

	private float enemyHealth;
    private float max_enemyHealth;
	private int enemyGold;
	private int enemyScore;
	private float enemySpeed;
	private char enemyElement;
	public int level;
    private bool isPoisoning;
    private bool isFreezing;
    private float remainingTime;

    public GameObject healthBar;    //healthBar
    private GameObject my_health;   //initial healthBar

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	float pathNodeOffset = 0.2f;

	// Use this for initialization
	void Start () {
        isFreezing = false;
        isPoisoning = false;
		if(level==1){
			max_enemyHealth=10f;
			enemyGold=5;
			enemyScore=3;
			enemySpeed=1f/*6f*/;
			enemyElement='g';
		}
		if(level==2){
			max_enemyHealth=20f;
			enemyGold=10;
			enemyScore=6;
			enemySpeed=1f;
			enemyElement='m';
		}
		if(level==3){
            max_enemyHealth = 30f;
			enemyGold=20;
			enemyScore=12;
			enemySpeed=6f;
			enemyElement='e';
		}
		if(level==4){
            max_enemyHealth = 40f;
			enemyGold=40;
			enemyScore=24;
			enemySpeed=6f;
			enemyElement='w';
		}
		if(level==5){
            max_enemyHealth = 50;
			enemyGold=50;
			enemyScore=36;
			enemySpeed=6f;
			enemyElement='f';
		}

        //health Bar
        enemyHealth = max_enemyHealth;
        my_health = (GameObject)Instantiate(healthBar, this.transform.position, this.transform.rotation);
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
        checkStatus(level);
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

        //Update Health Bar Location.
        my_health.GetComponent<Transform>().position = new Vector3(
           Camera.main.WorldToScreenPoint(this.transform.position).x,
           Camera.main.WorldToScreenPoint(this.transform.position).y+75f,
           0
        );
	}

    // Set Health Bar
    public void SetHealthBar(float calc_health)
    {
        my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1, 1);
    }

	void ReachedGoal() {
		GameObject.FindObjectOfType<HPManager>().LoseLife();
        SpawnerManager.stillAlive--;
		Destroy(gameObject);
        Destroy(my_health);
	}

	public void TakeDamage(float damage, char element) {

    //set status for wood or water bullet.
    if (element == 'm' || element == 'w') changeStatus(element);

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
		Debug.Log ("Enemy takes "+damage+" damage!");

		enemyHealth -= damage;
        //calculate health bar and set
        float calc_health = enemyHealth / max_enemyHealth;
        SetHealthBar(calc_health);

		if(enemyHealth <= 0) {
			Die(element);
		}
	}

	public void Die(char element) {
		// TODO: Do this more safely!
		if(element=='g'){
			enemyGold=enemyGold*2;
		}
		ScoreManager.score += enemyScore; //+enemyscore
		GoldManager.gold += enemyGold; //+enemygold
    SpawnerManager.stillAlive--;
		Destroy(gameObject);
        Destroy(my_health);
	}

    public void changeStatus(char element)
    {
        if (element == 'm')
        {
            isPoisoning = true;
            remainingTime = 5.0f;
            Material test = (Material)Resources.Load("poisoning");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
        if (element == 'w')
        {
            if (!isFreezing) enemySpeed = 2 * enemySpeed / 3;
            isFreezing = true;
            remainingTime = 5.0f;
            Material test = (Material)Resources.Load("freezing");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
    }

    public void checkStatus(int num)
    {
        if (isPoisoning)
        {
            if (remainingTime <= 0)
            {
                isPoisoning=false;
                remainingTime = 0;
                if (isFreezing) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("freezing");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+num);
            }
            else
            {
                enemyHealth -= (1 * Time.deltaTime);
                remainingTime -= (1 * Time.deltaTime);
                float calc_health = enemyHealth / max_enemyHealth;
                SetHealthBar(calc_health);
                Debug.Log(enemyHealth);
            }
        }
        if (isFreezing)
        {
            if (remainingTime <= 0)
            {
                isFreezing = false;
                remainingTime = 0;
                if (isPoisoning) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("poisoning");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+num);
                enemySpeed = 3 * enemySpeed / 2;
            }
            else
            {
                remainingTime -= (1 * Time.deltaTime);
                Debug.Log(enemyHealth);
            }
        }
    }
}
=======
using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour {

	private float enemyHealth;
    private float max_enemyHealth;
	private int enemyGold;
	private int enemyScore;
	private float enemySpeed;
	private char enemyElement;
	public int level;
    private bool isPoisoning;
    private bool isFreezing;
    private float remainingTime;

    public GameObject healthBar;    //healthBar
    private GameObject my_health;   //initial healthBar

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	float pathNodeOffset = 0.2f;

	// Use this for initialization
	void Start () {
        isFreezing = false;
        isPoisoning = false;
		if(level==1){
			max_enemyHealth=10f;
			enemyGold=5;
			enemyScore=3;
			enemySpeed=1f/*6f*/;
			enemyElement='g';
		}
		if(level==2){
			max_enemyHealth=20f;
			enemyGold=10;
			enemyScore=6;
			enemySpeed=1f;
			enemyElement='m';
		}
		if(level==3){
            max_enemyHealth = 30f;
			enemyGold=20;
			enemyScore=12;
			enemySpeed=6f;
			enemyElement='e';
		}
		if(level==4){
            max_enemyHealth = 40f;
			enemyGold=40;
			enemyScore=24;
			enemySpeed=6f;
			enemyElement='w';
		}
		if(level==5){
            max_enemyHealth = 50;
			enemyGold=50;
			enemyScore=36;
			enemySpeed=6f;
			enemyElement='f';
		}

        //health Bar
        enemyHealth = max_enemyHealth;
        my_health = (GameObject)Instantiate(healthBar, this.transform.position, this.transform.rotation);
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
        checkStatus(level);
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

        //Update Health Bar Location.
        my_health.GetComponent<Transform>().position = new Vector3(
           Camera.main.WorldToScreenPoint(this.transform.position).x,
           Camera.main.WorldToScreenPoint(this.transform.position).y+75f,
           0
        );
	}

    // Set Health Bar
    public void SetHealthBar(float calc_health)
    {
        my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1, 1);
    }

	void ReachedGoal() {
		GameObject.FindObjectOfType<HPManager>().LoseLife();
        SpawnerManager.stillAlive--;
		Destroy(gameObject);
        Destroy(my_health);
	}

	public void TakeDamage(float damage, char element) {

    //set status for wood or water bullet.
    if (element == 'm' || element == 'w') changeStatus(element);

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
		Debug.Log ("Enemy takes "+damage+" damage!");

		enemyHealth -= damage;
        //calculate health bar and set
        float calc_health = enemyHealth / max_enemyHealth;
        SetHealthBar(calc_health);

		if(enemyHealth <= 0) {
			Die(element);
		}
	}

	public void Die(char element) {
		// TODO: Do this more safely!
		if(element=='g'){
			enemyGold=enemyGold*2;
		}
		ScoreManager.score += enemyScore; //+enemyscore
		GoldManager.gold += enemyGold; //+enemygold
    SpawnerManager.stillAlive--;
		Destroy(gameObject);
        Destroy(my_health);
	}

    public void changeStatus(char element)
    {
        if (element == 'm')
        {
            isPoisoning = true;
            remainingTime = 5.0f;
            Material test = (Material)Resources.Load("poisoning");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
        if (element == 'w')
        {
            if (!isFreezing) enemySpeed = 2 * enemySpeed / 3;
            isFreezing = true;
            remainingTime = 5.0f;
            Material test = (Material)Resources.Load("freezing");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
    }

    public void checkStatus(int num)
    {
        if (isPoisoning)
        {
            if (remainingTime <= 0)
            {
                isPoisoning=false;
                remainingTime = 0;
                if (isFreezing) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("freezing");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+num);
            }
            else
            {
                enemyHealth -= (1 * Time.deltaTime);
                remainingTime -= (1 * Time.deltaTime);
                float calc_health = enemyHealth / max_enemyHealth;
                SetHealthBar(calc_health);
                Debug.Log(enemyHealth);
            }
        }
        if (isFreezing)
        {
            if (remainingTime <= 0)
            {
                isFreezing = false;
                remainingTime = 0;
                if (isPoisoning) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("poisoning");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+num);
                enemySpeed = 3 * enemySpeed / 2;
            }
            else
            {
                remainingTime -= (1 * Time.deltaTime);
                Debug.Log(enemyHealth);
            }
        }
    }
}
>>>>>>> afca6db4f48cd66893e401352b463f83b5c72567
