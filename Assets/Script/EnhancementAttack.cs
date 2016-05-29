using UnityEngine;
using System.Collections;

public class EnhancementAttack : MonoBehaviour {

	public GameObject projectileX;
	public float rechargeRate = 10.0f;
	public Transform botSpawnTransform;
	public float damage;
	public float radius;

	private float nextAttackTime;
	private bool isEnergyFull;
	//private FindNearestEnemy nearestEnemyFinding;
	//private Vector3 nearestEnemyDir;

	// Use this for initialization
	void Start () {
		nextAttackTime = Time.time + rechargeRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextAttackTime) {
			isEnergyFull = true;
		}
		if (isEnergyFull) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
		if (!isEnergyFull) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", Color.gray);
		}
	}

	void OnMouseUpAsButton()
	{
		Debug.Log("Click the tower to attack!");

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

		if (isEnergyFull) {
			//Instantiate (projectileX, botSpawnTransform.position, botSpawnTransform.rotation);

			ShootAt(nearestEnemy);
			isEnergyFull = false;
			nextAttackTime = Time.time + rechargeRate;
		}
	}

	void ShootAt(Enemy e) {
		// TODO: Fire out the tip!
		GameObject bulletGO = (GameObject)Instantiate(projectileX, botSpawnTransform.position, botSpawnTransform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}
}
