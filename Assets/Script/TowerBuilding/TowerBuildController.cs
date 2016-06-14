using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildController : MonoBehaviour {

	public GameObject[] towers; 
	public GameObject towerSelectionPanel;
	public float yOffset;

	GameTouchHandler gameTouch;
	List<string> occupiedTowerSpots = new List<string> ();
	RaycastHit myHit;
	GameObject selectedTower;
	GameObject myTowerSelectionPanel;

	void Start () {
		gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler> ();
	}
	
	void Update () {
        if (/*towerSpotTouch*/gameTouch.isTowerSpotTapped /*&& !towerBodyTouch.isTowerBodyTapped*/ && Time.timeScale!=0) {
			myHit = gameTouch.hit;
			if (!occupiedTowerSpots.Contains (/*towerSpotTouch*/myHit.collider.name)) {
				// if the tower spot is empty, means not occupied by tower
				myTowerSelectionPanel = (GameObject)Instantiate(towerSelectionPanel, 
					new Vector3(
						myHit.transform.position.x,
						myHit.transform.position.y + yOffset,
						myHit.transform.position.z
					),
					myHit.transform.rotation
				);
				//moveTowerSelectionPanel2HitPoint();
				myTowerSelectionPanel.SetActive (true);
			}
		}

		if (isTowerSelectionConfirmed()) {
			Destroy(myTowerSelectionPanel);
            if (canBuildTower ()) {
				GoldManager.gold -= selectedTower.GetComponent<TowerData> ().cost;
				Instantiate (selectedTower, myHit.transform.position, myHit.transform.rotation);
				occupiedTowerSpots.Add (/*towerSpotTouch.hitColliderName*/myHit.collider.name);
			}
		}
//        else if (towerSpotTouch.isBlank&&!isInsideSelectionPanel())
//        {
//            towerSelectPanel.gameObject.SetActive(false);
//        }

		if (gameTouch.isGameEnvironmentTapped) {
			//towerSelectPanel.gameObject.SetActive(false);
			Destroy(myTowerSelectionPanel);

		}

		//towerSelectPanel.isTowerSelected = false;
	}

//    void moveTowerSelectionPanel2HitPoint()
//    {
//        // move the tower selection panel to the touch point
//        //towerSelectPanel.init = true;
//        //towerSelectPanel.distance = 0;
////        for (int i = 0; i < 5; i++)
////        {
////            towerSelectionImageTrans[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
////				myCamera.WorldToScreenPoint(myHit.transform.position).x,//towerSpotTouch.touchPos.x,
////				myCamera.WorldToScreenPoint(myHit.transform.position).y //towerSpotTouch.touchPos.y
////            );
////        }
//		TSCanvas = GameObject.Find ("TowerSelectionPanel/TSCanvas").transform;
//
//		TSCanvas.GetComponent<RectTransform>().position = new Vector3 (
//			myHit.transform.position.x,
//			myHit.transform.position.y + yOffset,
//			myHit.transform.position.z
//		);
//    }

	bool canBuildTower()
	{
		return GoldManager.gold >= selectedTower.GetComponent<TowerData> ().cost;
	}

//    bool isInsideSelectionPanel()
//    {
//        bool result = false;
//        Vector3 dis0 = towerSelectionImageTrans[0].GetComponent<RectTransform>().position - Input.mousePosition;
//        for(int i = 0; i < 5; i++)
//        {
//            if ((towerSelectionImageTrans[i].GetComponent<RectTransform>().position - Input.mousePosition).magnitude < 30) result = true;
//        }
//
//        return result;
//    }

	bool isTowerSelectionConfirmed()
	{
		bool isSelected = false;

		if (gameTouch.isEarthTowerSelected) {
			isSelected = true;
			selectedTower = towers [0];
		} else if (gameTouch.isFireTowerSelected) {
			isSelected = true;
			selectedTower = towers [1];
		} else if (gameTouch.isMetalTowerSelected) {
			isSelected = true;
			selectedTower = towers [2];
		} else if (gameTouch.isWaterTowerSelected) {
			isSelected = true;
			selectedTower = towers [3];
		} else if (gameTouch.isWoodTowerSelected) {
			isSelected = true;
			selectedTower = towers [4];
		} else
			isSelected = false;

		return isSelected;
	}
}
