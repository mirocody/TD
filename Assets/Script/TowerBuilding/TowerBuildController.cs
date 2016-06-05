using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildController : MonoBehaviour {

	public GameObject towerSelectionPanel;

	TowerSelectionPanel towerSelectPanel;
	TowerSpotTouch towerSpotTouch;
	string hitColliderName;
	Transform towerSelectionImageTrans;
	List<string> occupiedTowerSpots = new List<string> ();

	void Start () {
		towerSelectPanel = transform.Find ("TowerSelectionPanel").GetComponent<TowerSelectionPanel>();
		towerSelectionImageTrans = transform.Find("TowerSelectionPanel/TSCanvas/TSImage");
		towerSpotTouch = GetComponent<TowerSpotTouch> ();
	}
	
	void Update () {

		if (towerSpotTouch.isTowerSpotTapped) {
			if (!occupiedTowerSpots.Contains (towerSpotTouch.hitColliderName)) {
				// if the tower spot is clear, means not occupied by tower
				moveTowerSelectionPanel2TouchPos();
				towerSelectPanel.gameObject.SetActive (true);
			}
		}

		if (towerSelectPanel.isTowerSelected) {
			towerSelectPanel.gameObject.SetActive (false);
			if (canBuildTower ()) {
				GoldManager.gold -= towerSelectPanel.selectedTower.GetComponent<TowerData> ().cost;
				//Debug.Log ("Tower selected is: " + towerSelectPanel.selectedTower.tag);
				Instantiate (
					towerSelectPanel.selectedTower, 
					GameObject.Find(towerSpotTouch.hitColliderName).transform.position, 
					GameObject.Find(towerSpotTouch.hitColliderName).transform.rotation
				);
				//Debug.Log ("Instantiate tower tag is: " + tower.tag);
				occupiedTowerSpots.Add (towerSpotTouch.hitColliderName);
			}
		}

		towerSelectPanel.isTowerSelected = false;
	}

	void moveTowerSelectionPanel2TouchPos()
	{
		// move the tower selection panel to the touch point
		towerSelectionImageTrans.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
			towerSpotTouch.touchPos.x, 
			towerSpotTouch.touchPos.y
		);
	}

	bool canBuildTower()
	{
		//return GoldManager.gold >= towerSelectPanel.selectedTower.GetComponent<TowerData> ().cost;
		return true;
	}
}
