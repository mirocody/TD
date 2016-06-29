using UnityEngine;
using System.Collections;

public class TowerElevation : MonoBehaviour {

	TowerData towerData;

	// Use this for initialization
	void Start () {
		towerData = GetComponent<TowerData> ();
	}
	
	// Update is called once per frame
	void Update () {
		// one concern is worry about the performance of the game, 'cause if will detect all towers and projectiles in the range every frame

		Collider[] colliders = Physics.OverlapSphere(transform.position, towerData.elevateRadius);
		if(colliders.Length > 1)
		{
			foreach (Collider c in colliders) {
				// increase the performance of towers within the range (which is defined by elevateRadius) except itself
				if (c.tag == "TowerBody" && c != GetComponent<Collider>()) {
					if (!c.GetComponent<TowerData> ().isElevated) {
						TowerData otherTowerData = c.GetComponent<TowerData> ();

						otherTowerData.elevateRange = c.GetComponent<TowerData> ().range * towerData.elevateRate;
						otherTowerData.range += otherTowerData.elevateRange;

						otherTowerData.elevateTurnSpeed = c.GetComponent<TowerData> ().turnSpeed * towerData.elevateRate;
						otherTowerData.turnSpeed += otherTowerData.elevateTurnSpeed;

						otherTowerData.elevateErrorAmount = c.GetComponent<TowerData> ().errorAmount * towerData.elevateRate;
						otherTowerData.errorAmount -= otherTowerData.elevateErrorAmount;

						otherTowerData.elevateFireCoolDown = c.GetComponent<TowerData> ().fireCoolDown * towerData.elevateRate;
						otherTowerData.fireCoolDown -= otherTowerData.elevateFireCoolDown;

						//otherTowerData.elevateRechangeRate = c.GetComponent<TowerData> ().errorAmount * towerData.elevateRate;
						//c.GetComponent<TowerData> ().rechargeRate -= otherTowerData.elevateRechangeRate;
						if(c.transform.Find("Light"))
							c.transform.Find("Light").GetComponent<Light> ().enabled = true;
						otherTowerData.isElevated = true;
					}

				}
				// increase the damage of projectiles within the range which is defined by elevateRadius
				if (c.tag == "Projectiles") {
					if (!c.GetComponent<Projectile> ().isElevate) {
						c.GetComponent<Projectile> ().speed += c.GetComponent<Projectile> ().speed * towerData.elevateRate;
						c.GetComponent<Projectile> ().damage += c.GetComponent<Projectile> ().damage * towerData.elevateRate;
						c.GetComponent<Projectile> ().radius += c.GetComponent<Projectile> ().radius * towerData.elevateRate;
						c.GetComponent<Projectile> ().isElevate = true;
					}
				}
			}
		}
	}
}
