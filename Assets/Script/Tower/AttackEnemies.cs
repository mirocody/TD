using UnityEngine;
using System.Collections;

public class AttackEnemies : MonoBehaviour {

	public GameObject projectile;
	public Transform botSpawnTransform;
	public Transform towerBody;

	private AttackStrategies attackStrategy;
	private TowerData towerData;
	private float aimError;

	// Use this for initialization
	void Start () {
		attackStrategy = gameObject.GetComponent<AttackStrategies>();
		towerData = gameObject.GetComponent<TowerData> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(attackStrategy.targetEnemy == null) {
			Debug.Log("No enemies?");
			return;
		}

		// Turn to the target enemy
		Vector3 relativePos = attackStrategy.targetEnemy.position - this.transform.position;
		Vector3 aimPoint = relativePos + new Vector3 (aimError, aimError, aimError);
		Quaternion desiredRotation = Quaternion.LookRotation( aimPoint );
		towerBody.rotation = Quaternion.Lerp(towerBody.rotation, desiredRotation, Time.deltaTime * towerData.turnSpeed);

		// Attack target enemy
		towerData.fireCooldownLeft -= Time.deltaTime;
		if(towerData.fireCooldownLeft <= 0 && relativePos.magnitude <= towerData.range) {
			towerData.fireCooldownLeft = towerData.fireCooldown;
			AttackAt(attackStrategy.targetEnemy);
		}

	}

	void CalculateAimError()
	{
		aimError = Random.Range(-towerData.errorAmount, towerData.errorAmount);	
	}

	void AttackAt(Transform trans){
		GameObject bulletGO = (GameObject)Instantiate(projectile, this.transform.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = trans;
	}
}
