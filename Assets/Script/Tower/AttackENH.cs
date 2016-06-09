using UnityEngine;
using System.Collections;

public class AttackENH : MonoBehaviour {

	public GameObject projectileX;
	public Transform botSpawnTransform;
	public Transform towerBody;

	private float nextAttackTime;
	private bool isEnergyFull;
	private TowerData towerData;
	private AttackStrategies attackStrategy;
	private float aimError;

	// Use this for initialization
	void Start () {
		towerData = transform.parent.gameObject.GetComponent<TowerData> ();
		attackStrategy = transform.parent.gameObject.GetComponent<AttackStrategies> ();
		nextAttackTime = Time.time + towerData.rechargeRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextAttackTime) {
			isEnergyFull = true;
		}
		if (isEnergyFull) {
			//transform.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
		if (!isEnergyFull) {
			//transform.GetComponent<Renderer> ().material.SetColor ("_Color", Color.gray);
		}
	}

	void OnMouseUpAsButton()
	{
		Debug.Log ("Tap the tower to trigger enhanced attack!");

		if (attackStrategy.targetEnemy == null) {
			Debug.Log ("No enemies?");
			return;
		}

		// Turn to the target enemy
		Vector3 relativePos = attackStrategy.targetEnemy.position - this.transform.position;
		Vector3 aimPoint = relativePos + new Vector3 (aimError, aimError, aimError);
		Quaternion desiredRotation = Quaternion.LookRotation (aimPoint);
		towerBody.rotation = Quaternion.Lerp (towerBody.rotation, desiredRotation, Time.deltaTime * towerData.turnSpeed);

		// Attack target enemy
		if (isEnergyFull && relativePos.magnitude <= towerData.range) {
			AttackAt (attackStrategy.targetEnemy);
			isEnergyFull = false;
			nextAttackTime = Time.time + towerData.rechargeRate;
		}
	}

	void CalculateAimError()
	{
		aimError = Random.Range(-towerData.errorAmount, towerData.errorAmount);	
	}

	void AttackAt(Transform trans) {
		GameObject bulletGO = (GameObject)Instantiate(projectileX, botSpawnTransform.position, botSpawnTransform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = trans.transform;
	}
}
