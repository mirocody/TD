using UnityEngine;
using System.Collections;

public class ShootEnemies : MonoBehaviour {

	public GameObject projectile;
	public float range = 10f;
	public int cost = 5;
	public float damage = 1;
	public float radius = 0;

	public Transform botSpawnTransform;
	public Transform towerBody;
	public float turnSpeed = 5.0f;

	public float rechargeRate = 10.0f;
	public float errorAmount = 1.0f;

	private Transform targetTransform;
	private float nextFireTime;
	private Quaternion desiredRotation;
	private float aimError;

	private float fireCooldown = 0.5f;
	private float fireCooldownLeft = 0;

	//private FindNearestEnemy nearestEnemyFinding;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			targetTransform = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform == targetTransform) {
			targetTransform = null;
		}
	}

	// Use this for initialization
	void Start () {
		nextFireTime = Time.time;
		//nearestEnemyFinding = transform.GetComponent<FindNearestEnemy> ();
	}
	
	// Update is called once per frame
	void Update () {
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

		Enemy nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach(Enemy e in enemies) {
			float d = Vector3.Distance(this.transform.position, e.transform.position);
			if(nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		//nearestEnemyFinding = transform.GetComponent<FindNearestEnemy> ();
			
		if(nearestEnemy == null) {
			Debug.Log("No enemies?");
			return;
		}

		Vector3 dir = nearestEnemy.transform.position - this.transform.position;

		//Vector3 relativePos = nearestEnemyFinding.dir;

		Quaternion lookRot = Quaternion.LookRotation( dir );

		//Debug.Log(lookRot.eulerAngles.y);
		towerBody.rotation = Quaternion.Euler( 0, lookRot.eulerAngles.y, 0 );

		fireCooldownLeft -= Time.deltaTime;
		if(fireCooldownLeft <= 0 && dir.magnitude <= range) {
			fireCooldownLeft = fireCooldown;
			ShootAt(nearestEnemy);
		}

	}

	void CalculateAimError()
	{
		aimError = Random.Range(-errorAmount, errorAmount);	
	}

	void ShootAt(Enemy e) {
		// TODO: Fire out the tip!
		GameObject bulletGO = (GameObject)Instantiate(projectile, this.transform.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}
}
