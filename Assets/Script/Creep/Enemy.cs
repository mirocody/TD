using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Transform targetPathNode;
	public int pathNodeIndex = 0;

	protected float enemyHealth;
	protected float max_enemyHealth;
	protected int enemyGold;
	protected int enemyScore;
	protected float enemySpeed;
	private float originSpeed;
	protected char enemyElement;
	public int level;
    private bool isPoisoning;
    private bool isFreezing;
	private  bool isStunned;
    private float remainingPoisonTime;
		private float remainingFreezeTime;
		private float remainingStunTime;
		public int freezePercent;
		public int poisonDamage;


	public GameObject healthBar;    //healthBar
	protected GameObject my_health;   //initial healthBar

	public GameObject star;	//dizzy
	protected GameObject my_star; //initial dizzy

	//sound
	public AudioClip dieSound;

    public GameObject selectedArrow; //Arrow for selected enemy.
    public GameObject my_Arrow;//initial Arrow.

	GameObject pathGO;
	float pathNodeOffset = 0.2f;
	InitialData initData;
	InstantTransferEnemy instantTE;

	// Use this for initialization
	void Start () {
        isFreezing = false;
        isPoisoning = false;
		//pathGO = GameObject.Find("Path");
		initData = GameObject.Find("SystemData").GetComponent<InitialData>();

		if(level==1){
			max_enemyHealth = initData.enemyhealth[0];
			enemyGold = initData.enemygold[0];
			enemyScore= initData.enemyscore[0];
			enemySpeed= initData.enemyspeed[0];
			enemyElement='g';
		}
		if(level==2){
			max_enemyHealth = initData.enemyhealth[1];
			enemyGold= initData.enemygold[1];
			enemyScore= initData.enemyscore[1];
			enemySpeed= initData.enemyspeed[1];
			enemyElement='m';
		}
		if(level==3){
			max_enemyHealth = initData.enemyhealth[2];
			enemyGold= initData.enemygold[2];
			enemyScore= initData.enemyscore[2];
			enemySpeed= initData.enemyspeed[2];
			enemyElement='e';
		}
		if(level==4){
			max_enemyHealth = initData.enemyhealth[3];
			enemyGold= initData.enemygold[3];
			enemyScore= initData.enemyscore[3];
			enemySpeed= initData.enemyspeed[3];
			enemyElement='w';
		}
		if(level==5){
			max_enemyHealth = initData.enemyhealth[4];
			enemyGold=initData.enemygold[4];
			enemyScore= initData.enemyscore[4];
			enemySpeed= initData.enemyspeed[4];
			enemyElement='f';
		}
		originSpeed = enemySpeed;
		//health Bar
		enemyHealth = max_enemyHealth;
		my_health = (GameObject)Instantiate(healthBar, this.transform.position, this.transform.rotation);

        //SelectedArrow.
        my_Arrow = (GameObject)Instantiate(selectedArrow, this.transform.position, this.transform.rotation);
        my_Arrow.SetActive(false);

		instantTE = GetComponent<InstantTransferEnemy> ();

		//initial dizzy star
		my_star = (GameObject)Instantiate(star, this.transform.position, this.transform.rotation);
		star.SetActive (false);

	}

	void GetNextPathNode() {
		if(pathNodeIndex < NewMap.pathPoints.GetLength(0)) {
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

		if (!instantTE.instantTransferMode && !isStunned) {
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

	        //Update Arrow Location.
	        my_Arrow.GetComponent<Transform>().position = new Vector3(
	            Camera.main.WorldToScreenPoint(this.transform.position).x,
	            Camera.main.WorldToScreenPoint(this.transform.position).y + 70f,
	            0
	        );

			//Update Dizzy location
			my_star.GetComponent<Transform> ().position = new Vector3 (
				Camera.main.WorldToScreenPoint(this.transform.position).x,
				Camera.main.WorldToScreenPoint(this.transform.position).y + 50f,
				0
			);
    	}

		//Update Health Bar Location.
		my_health.GetComponent<Transform>().position = new Vector3(
			Camera.main.WorldToScreenPoint(this.transform.position).x,
			Camera.main.WorldToScreenPoint(this.transform.position).y+40f,
			0
		);
	}

	// Set Health Bar
	public virtual void SetHealthBar(float calc_health)
	{
		my_health.transform.Find("HealthCanvas/Health").GetComponent<RectTransform>().localScale = new Vector3 (calc_health, 1, 1);
	}

	void ReachedGoal() {
		GameObject.FindObjectOfType<HPManager>().LoseLife();
        SpawnerManager.stillAlive--;
		Destroy(gameObject);
		Destroy(my_health);
        Destroy(my_Arrow);
        if (TargetEnemyController.targetEnemey == this.transform)
        {
            TargetEnemyController.targetEnemey = null;
        }
    }

	public virtual void TakeDamage(float damage, char towerElement,int level,bool stunFlag) {
    //set status for wood or water bullet.
    if (towerElement == 'm' || towerElement == 'w'|| towerElement == 'r'|| towerElement == 'd')
		Debug.Log("Change stun damage"+stunFlag);

			changeStatus(towerElement,stunFlag);

	//gold enemy
		if(enemyElement=='g'){
			if(towerElement=='f'||enemyElement=='q'){
				damage=2*damage;
			}
			else if(towerElement=='m'){
				damage=damage/2;
            }
		}
	//wood enemy
		if(enemyElement=='m'){
			if(towerElement=='g'||towerElement=='r'||towerElement=='d'){
				damage=2*damage;
			}
			else if(towerElement=='e'){
				damage=damage/2;
			}
		}
	//earth enemy
		if(enemyElement=='e'){
			if(towerElement=='m'||towerElement=='q'||towerElement=='y'){
				damage=2*damage;
			}
			else if(towerElement=='w'){
				damage=damage/2;
			}
		}
	//water enemy
		if(enemyElement=='w'){
			if(towerElement=='e'||towerElement=='r'){
				damage=2*damage;
			}
			else if(towerElement=='f'){
				damage=damage/2;
			}
		}
		//fire enemy
		if(enemyElement=='f'){
			if(towerElement=='w'||enemyElement=='d'||enemyElement=='y'){
				damage=2*damage;
			}
			else if(towerElement=='g'){
				damage=damage/2;
			}
		}
//		Debug.Log ("Enemy takes "+damage+"damage");

		enemyHealth -= damage;
		float calc_health = enemyHealth / max_enemyHealth;
		SetHealthBar(calc_health);

		if(enemyHealth <= 0) {
			Die(towerElement);
		}
	}

	public virtual void Die(char towerElement) {
        // TODO: Do this more safely!
		if(towerElement=='g'){
			enemyGold=enemyGold*2;
		}
		ScoreManager.score += enemyScore; //+enemyscore
		GoldManager.gold += enemyGold; //+enemygold
    	SpawnerManager.stillAlive--;
    	//Debug.Log(this.transform.name+" Alive in Enemy:" + SpawnerManager.stillAlive);
		Destroy(gameObject);
		Destroy(my_health);
        Destroy(my_Arrow);
		Destroy (my_star);
        if (TargetEnemyController.targetEnemey == this.transform)
        {
            TargetEnemyController.targetEnemey = null;
        }
		AudioSource.PlayClipAtPoint (dieSound, this.transform.localPosition);
	}

    public void changeStatus(char towerElement,bool stunFlag)
    {
        if (towerElement == 'm')
        {

            isPoisoning = true;
            remainingPoisonTime = 5.0f;
            Material test = (Material)Resources.Load("poisoning");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
        if (towerElement == 'w'||towerElement == 'd')
        {
            if (!isFreezing) enemySpeed = enemySpeed*(float)(100-freezePercent)/100;
            isFreezing = true;
            remainingFreezeTime = 5.0f;
            Material test = (Material)Resources.Load("freezing");
            transform.Find("Body").GetComponent<Renderer>().material = test;
        }
		if(stunFlag==true)
			{
			isStunned=true;
			Debug.Log("change status== "+stunFlag);
			remainingStunTime=3.0f;

			}
    }

    public void checkStatus(int enemyLevel)
    {
        if (isPoisoning)
        {
            if (remainingPoisonTime <= 0)
            {
                isPoisoning=false;
                remainingPoisonTime = 0;
                if (isFreezing) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("freezing");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+enemyLevel);
				//my_star.SetActive (false);
			}
            else
            {
				enemyHealth -= ((float)poisonDamage * Time.deltaTime);
				//set dizzy star to be active
				//my_star.SetActive(true);

        		remainingPoisonTime -= (1 * Time.deltaTime);
				float calc_health = enemyHealth / max_enemyHealth;
				SetHealthBar(calc_health);
				//Debug.Log(enemyHealth);
							if(enemyHealth <= 0) {
									Die('m');
							}
            }
        }
        
		if (isFreezing)
        {
            if (remainingFreezeTime <= 0)
            {
                isFreezing = false;
                remainingFreezeTime = 0;
                if (isPoisoning) transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("poisoning");
                else transform.Find("Body").GetComponent<Renderer>().material = (Material)Resources.Load("creep_"+enemyLevel);
                enemySpeed = originSpeed;
            }
            else
            {
                remainingFreezeTime -= (1 * Time.deltaTime);
            }
        }

		if(isStunned)
		{
			if (remainingStunTime <= 0)
			{
					isStunned = false;
					remainingStunTime = 0;
					enemySpeed = originSpeed;
					my_star.SetActive (false);
			}
			else
			{
					enemySpeed=0;
					remainingStunTime -= (1 * Time.deltaTime);
					my_star.SetActive (true);
					Debug.Log("Rotation starts");
					transform.Rotate(Vector3.up * Time.deltaTime * 150);
			}
		}
    }
}
