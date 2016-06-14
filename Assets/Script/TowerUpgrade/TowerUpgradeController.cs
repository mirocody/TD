using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerUpgradeController : MonoBehaviour {

	public float yOffset;

	public GameObject[] MetalTowers;
	public GameObject[] WoodTowers;
	public GameObject[] WaterTowers;
	public GameObject[] FireTowers;
	public GameObject[] EarthTowers;

	public GameObject towerUpgradePanel;

	[HideInInspector]
	public int upgradeCost;

	//TowerUpgradePanel towerUpgradePanel;
	//TowerBodyTouch towerBodyTouch;
	GameTouchHandler gameTouch;
	//Transform towerUpgradeContainerTrans;
	//Camera myCamera;
	RaycastHit myHit;
	GameObject myTowerUpgradePanel;

	void Start () {
		//towerUpgradePanel = transform.Find ("TowerUpgradePanel").GetComponent<TowerUpgradePanel>();
		//towerUpgradeContainerTrans = transform.Find("TowerUpgradePanel/TUCanvas/TUContainer");
		//towerBodyTouch = GetComponent<TowerBodyTouch> ();
		gameTouch = GameObject.Find ("GameTouch").GetComponent<GameTouchHandler> ();
		//myCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();
	}
	
	void Update () {
		if (gameTouch.isTowerBodyTapped) {
			myHit = gameTouch.hit;
			upgradeCost = myHit.transform.gameObject.GetComponent<TowerData> ().upgradeCost;

			myTowerUpgradePanel = (GameObject)Instantiate(towerUpgradePanel, 
				new Vector3(
					myHit.transform.position.x,
					myHit.transform.position.y + yOffset,
					myHit.transform.position.z
				),
				myHit.transform.rotation
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

		if (gameTouch.isGameEnvironmentTapped)
		{
			Destroy(myTowerUpgradePanel);
		}
	}

	public bool canUpgradeTower()
	{
		//Debug.Log ("upgrade cost is:" + upgradeCost);
		return GoldManager.gold >= upgradeCost && 
			myHit.transform.GetComponent<TowerData>().level < MetalTowers.GetLength(0);
	}
}
