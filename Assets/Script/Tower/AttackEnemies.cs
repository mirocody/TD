using UnityEngine;
using System.Collections;

public class AttackEnemies : MonoBehaviour {

	public GameObject projectile;
	public Transform botSpawnTransform;
	public Transform towerBody;

	private AttackStrategies attackStrategy;
	private TowerData towerData;
	private float aimError;
	private float fireCooldownLeft;

	// Use this for initialization
	void Start () {
		attackStrategy = gameObject.GetComponent<AttackStrategies>();
		towerData = gameObject.GetComponent<TowerData> ();
		towerData.init ();
	}
	
	// Update is called once per frame
	void Update () {

		if(attackStrategy.targetEnemy == null) {
			//Debug.Log("No enemies?");
			return;
		}

		// Turn to the target enemy
		Vector3 relativePos = attackStrategy.targetEnemy.position - this.transform.position;
        relativePos.y = 0;
		if(inAttackRange(relativePos,towerData.range)){
			Vector3 aimPoint = relativePos + new Vector3 (aimError, 0, aimError);
			Quaternion desiredRotation = Quaternion.LookRotation( aimPoint );
			towerBody.rotation = Quaternion.Lerp(towerBody.rotation, desiredRotation, Time.deltaTime * towerData.turnSpeed);
		}


		// Attack target enemy
		fireCooldownLeft -= Time.deltaTime;
		if(fireCooldownLeft <= 0 && relativePos.magnitude <= towerData.range) {
			fireCooldownLeft = towerData.fireCoolDown;
			AttackAt(attackStrategy.targetEnemy);
		}

	}

	bool inAttackRange(Vector3 relativePos,float range){
		float distance = relativePos.magnitude;
		if (distance > range)
			return false;
		else
			return true;
	
	}

	void CalculateAimError()
	{
		aimError = Random.Range(-towerData.errorAmount, towerData.errorAmount);	
	}

	void AttackAt(Transform trans){
		GameObject projectileGO = (GameObject)Instantiate(projectile, botSpawnTransform.position, this.transform.rotation);

		Projectile p = projectileGO.GetComponent<Projectile>();
		p.target = trans;
		//p.radius = towerData.radius;
	}
}
