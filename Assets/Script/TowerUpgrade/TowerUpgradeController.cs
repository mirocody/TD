using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerUpgradeController : MonoBehaviour {

	public int upgradeCost;

	TowerUpgradePanel towerUpgradePanel;
	TowerBodyTouch towerBodyTouch;
	Transform towerUpgradeImageTrans;
	//Transform costTxt;
	List<string> occupiedTowerSpots = new List<string> ();
	GameObject selectedTower;
	Camera myCamera;

	void Start () {
		towerUpgradePanel = transform.Find ("TowerUpgradePanel").GetComponent<TowerUpgradePanel>();
		towerUpgradeImageTrans = transform.Find("TowerUpgradePanel/TUCanvas/TUImage");
		towerBodyTouch = GetComponent<TowerBodyTouch> ();
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();
	}
	
	void Update () {
		if (towerBodyTouch.isTowerBodyTapped) {
			moveTowerUpgradePanel2TouchPos();
			upgradeCost = towerBodyTouch.hit.transform.gameObject.GetComponent<TowerData> ().getUpgradeCost (
				towerBodyTouch.hit.transform.gameObject.GetComponent<TowerData> ().level,
				towerBodyTouch.hit.collider.tag);
			towerUpgradePanel.gameObject.SetActive (true);
		}

		if (towerUpgradePanel.isTowerUpgradeConfirm) {
			towerUpgradePanel.gameObject.SetActive (false);
			towerBodyTouch.hit.transform.gameObject.GetComponent<TowerData> ().level++;
			GoldManager.gold -= upgradeCost;
			Debug.Log("Tower Upgrade success!");
		}

		towerBodyTouch.isTowerBodyTapped = false;
		towerUpgradePanel.isTowerUpgradeConfirm = false;
	}

	void moveTowerUpgradePanel2TouchPos()
	{
		// move the tower selection panel to the touch point
		towerUpgradeImageTrans.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).x, //towerBodyTouch.touchPos.x, 
			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).y  //towerBodyTouch.touchPos.y
		);
	}

	public bool canUpgradeTower()
	{
		Debug.Log ("upgrade cost is:" + upgradeCost);
		return GoldManager.gold >= upgradeCost;
	}
}
