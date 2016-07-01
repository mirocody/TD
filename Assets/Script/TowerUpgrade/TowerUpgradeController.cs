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
			myHit.transform.gameObject.GetComponent<TowerData> ().init ();
			upgradeCost = myHit.transform.gameObject.GetComponent<TowerData> ().upgradeCost;
			buildCost = myHit.transform.gameObject.GetComponent<TowerData> ().cost;

			// Find and Destroy existing upgrade panel(s) before instantiating new ones
			Destroy (myTowerUpgradePanel);

			GameObject[] existingTSPanels = GameObject.FindGameObjectsWithTag("TowerSelectionPanel");
			foreach (GameObject existingTSPanel in existingTSPanels) {
				Destroy (existingTSPanel);
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

				// New next level tower
				GameObject newTower;
				switch (myHit.transform.GetComponent<TowerData> ().towerType) {
				case 'g':
					newTower = (GameObject)Instantiate (
	                    MetalTowers [nextLevel - 1], // the array starts from 0, so the index of next level equals to nextLevel - 1
	                    myHit.transform.position,
	                    myHit.transform.rotation
	                );
					AddElevatedValueAfterUpgrade (newTower);
					break;
				case 'm':
					newTower = (GameObject)Instantiate (
						WoodTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					AddElevatedValueAfterUpgrade (newTower);
					break;
				case 'w':
					newTower = (GameObject)Instantiate (
						WaterTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					AddElevatedValueAfterUpgrade (newTower);
					break;
				case 'f':
					newTower = (GameObject)Instantiate (
						FireTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					AddElevatedValueAfterUpgrade (newTower);
					break;
				case 'e':
					newTower = (GameObject)Instantiate (
						EarthTowers [nextLevel - 1],
						myHit.transform.position,
						myHit.transform.rotation
					);
					AddElevatedValueAfterUpgrade (newTower);
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

			// before destroy the tower, eliminate instant transfer panel if it exists
			InstantTransfer it = myHit.transform.GetComponent<InstantTransfer>();
			if (it.myInstantTransferPanel)
				Destroy (it.myInstantTransferPanel);
			
			// before destroy the "Earth" tower, restore the elevate value for the surrounding towers 
			if (myHit.transform.GetComponent<TowerData> ().towerType == 'e') {
				colliders = Physics.OverlapSphere(myHit.transform.position, myHit.transform.GetComponent<TowerData>().elevateRadius);
				if (colliders.Length > 1) {
					foreach (Collider c in colliders) {
						if (c.tag == "TowerBody" && c != myHit.collider) {
							if (c.GetComponent<TowerData> ().isElevated) {
								RemoveElevatedValueAfterDestruction (c);
								//c.GetComponent<TowerData> ().rechargeRate += otherTowerData.elevateRechangeRate;
								if(c.transform.Find("Light"))
									c.transform.Find("Light").GetComponent<Light> ().enabled = false;
								c.GetComponent<TowerData> ().isElevated = false;
							}
						}
					}
				}
			}

			Destroy (myHit.transform.gameObject);
			GoldManager.gold += Mathf.RoundToInt(buildCost - buildCost * depreciation);

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

	void AddElevatedValueAfterUpgrade(GameObject newTower)
	{
		TowerData newTowerData = newTower.GetComponent<TowerData> ();
		if (newTowerData.isElevated) {
			newTowerData.range += newTowerData.elevateRange;
			newTowerData.turnSpeed += newTowerData.elevateTurnSpeed;
			newTowerData.errorAmount -= newTowerData.elevateErrorAmount;
			newTowerData.fireCoolDown -= newTowerData.elevateFireCoolDown;
		}
	}

	void RemoveElevatedValueAfterDestruction(Collider c)
	{
		TowerData otherTowerData = c.GetComponent<TowerData> ();
		// remove elevate values
		otherTowerData.range -=	otherTowerData.elevateRange;
		otherTowerData.turnSpeed -= otherTowerData.elevateTurnSpeed;
		otherTowerData.errorAmount += otherTowerData.elevateErrorAmount;
		otherTowerData.fireCoolDown += otherTowerData.elevateFireCoolDown;
	}
}
