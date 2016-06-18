using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerUpgradeController : MonoBehaviour {

	public float yOffset;
	public float depreciation;

	public GameObject[] MetalTowers;
	public GameObject[] WoodTowers;
	public GameObject[] WaterTowers;
	public GameObject[] FireTowers;
	public GameObject[] EarthTowers;

	public GameObject towerUpgradePanel;

	[HideInInspector]
	public int upgradeCost;

	GameTouchHandler gameTouch;
	TowerBuildController tBController;
	RaycastHit myHit;
	GameObject myTowerUpgradePanel;
	int buildCost;

	void Start () {
		gameTouch = GameObject.Find ("GameTouch").GetComponent<GameTouchHandler> ();
		tBController = GameObject.Find ("TowerBuild").GetComponent<TowerBuildController> ();
	}
	
	void Update () {
		if (gameTouch.isTowerBodyTapped) {
			myHit = gameTouch.hit;
			upgradeCost = myHit.transform.gameObject.GetComponent<TowerData> ().upgradeCost;
			buildCost = myHit.transform.gameObject.GetComponent<TowerData> ().cost;

			// Find and Destroy existing upgrade panel(s) before instantiating new ones
			GameObject[] existingTUPanels = GameObject.FindGameObjectsWithTag("TowerUpgradePanel");
			foreach (GameObject existingTUPanel in existingTUPanels) {
				Destroy (existingTUPanel);
			}

			myTowerUpgradePanel = (GameObject)Instantiate(towerUpgradePanel, 
				new Vector3(
					Camera.main.WorldToScreenPoint(myHit.transform.position).x, //myHit.transform.position.x,
					Camera.main.WorldToScreenPoint(myHit.transform.position).y + yOffset, //myHit.transform.position.y + yOffset,
					0 //myHit.transform.position.z
				),
				Quaternion.identity //myHit.transform.rotation
			);

			myTowerUpgradePanel.SetActive (true);
		}

		if (gameTouch.isTowerUpgradeConfirm) 
		{
			Destroy(myTowerUpgradePanel);

			if (canUpgradeTower()){
				GoldManager.gold -= upgradeCost;
				int nextLevel = myHit.transform.GetComponent<TowerData> ().level + 1;
				// Destroy the current tower, then new the next level tower
				Destroy (myHit.transform.gameObject);

				switch (myHit.transform.GetComponent<TowerData> ().towerType) {
				case 'g':
					Instantiate (
						MetalTowers [nextLevel - 1], // the array starts from 0, so the index of next level equals to nextLevel - 1
						myHit.transform.position,
						myHit.transform.rotation
					);
					break;
				case 'm':
					Instantiate (
						WoodTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					break;
				case 'w':
					Instantiate (
						WaterTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					break;
				case 'f':
					Instantiate (
						FireTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					break;
				case 'e':
					Instantiate (
						EarthTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					break;
				default:
					Debug.Log ("Tower type not found!");
					break;
				}
				Debug.Log ("Tower Upgrade success!");
			}
		}

		if (gameTouch.isTowerSoldConfirm) {
			Destroy(myTowerUpgradePanel);
			Collider[] colliders;

			// before destroy the "Earth" tower, restore the elevate value for the surrounding towers 
			if (myHit.transform.GetComponent<TowerData> ().towerType == 'e') {
				colliders = Physics.OverlapSphere(myHit.transform.position, myHit.transform.GetComponent<TowerData>().elevateRadius);
				if (colliders.Length > 1) {
					foreach (Collider c in colliders) {
						if (c.tag == "TowerBody" && c != myHit.collider) {
							if (c.GetComponent<TowerData> ().isElevated) {
								TowerData otherTowerData = c.GetComponent<TowerData> ();
								// restore elevate values
								otherTowerData.range -=	otherTowerData.elevateRange;
								otherTowerData.turnSpeed -= otherTowerData.elevateTurnSpeed;
								otherTowerData.errorAmount += otherTowerData.elevateErrorAmount;
								otherTowerData.fireCoolDown += otherTowerData.elevateFireCoolDown;
								//c.GetComponent<TowerData> ().rechargeRate += otherTowerData.elevateRechangeRate;
								c.transform.Find("Light").GetComponent<Light> ().enabled = false;
								otherTowerData.isElevated = false;
							}
						}
					}
				}
			}

			Destroy (myHit.transform.gameObject);
			GoldManager.gold += Mathf.RoundToInt(buildCost * depreciation);

			// if the tower was sold, remove its tower spot from the occupiedTowerSpots list so player can build new tower above it
			colliders = Physics.OverlapSphere(myHit.transform.position, 1.0f);
			if(colliders.Length > 1)
			{
				foreach (Collider c in colliders) {
					if (c.tag == "TowerSpot") {
						tBController.occupiedTowerSpots.Remove (c.name);
					}
				}
			}
		}

		if (gameTouch.isGameEnvironmentTapped)
		{
			Destroy(myTowerUpgradePanel);
		}
	}

	public bool canUpgradeTower()
	{
		return GoldManager.gold >= upgradeCost && 
			myHit.transform.GetComponent<TowerData>().level < MetalTowers.GetLength(0);
	}
}
