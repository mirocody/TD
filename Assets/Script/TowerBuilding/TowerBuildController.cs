using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildController : MonoBehaviour {

	public GameObject towerSelectionPanel;

	TowerSelectionPanel towerSelectPanel;
	TowerSpotTouch towerSpotTouch;
	string hitColliderName;
	Transform[] towerSelectionImageTrans;
	List<string> occupiedTowerSpots = new List<string> ();

	void Start () {
        towerSelectionImageTrans = new Transform[5];
		towerSelectPanel = transform.Find ("TowerSelectionPanel").GetComponent<TowerSelectionPanel>();
		towerSelectionImageTrans[0] = transform.Find("TowerSelectionPanel/TSCanvas/FireTower");
        towerSelectionImageTrans[1] = transform.Find("TowerSelectionPanel/TSCanvas/EarthTower");
        towerSelectionImageTrans[2] = transform.Find("TowerSelectionPanel/TSCanvas/WaterTower");
        towerSelectionImageTrans[3] = transform.Find("TowerSelectionPanel/TSCanvas/MetalTower");
        towerSelectionImageTrans[4] = transform.Find("TowerSelectionPanel/TSCanvas/WoodTower");
        towerSpotTouch = GetComponent<TowerSpotTouch> ();
	}
	
	void Update () {
        if (towerSpotTouch.isTowerSpotTapped&&Time.timeScale!=0) {
			if (!occupiedTowerSpots.Contains (towerSpotTouch.hitColliderName)) {
				// if the tower spot is clear, means not occupied by tower
				moveTowerSelectionPanel2TouchPos();
				towerSelectPanel.gameObject.SetActive (true);
			}
		}

		if (towerSelectPanel.isTowerSelected) {
			towerSelectPanel.gameObject.SetActive (false);
            Debug.Log("Touch the selection area.");
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
        else if (towerSpotTouch.isBlank&&!isInsideSelectionPanel())
        {
            towerSelectPanel.gameObject.SetActive(false);
        }

		towerSelectPanel.isTowerSelected = false;
	}

    void moveTowerSelectionPanel2TouchPos()
    {
        // move the tower selection panel to the touch point
        towerSelectPanel.init = true;
        towerSelectPanel.distance = 0;
        for (int i = 0; i < 5; i++)
        {
            towerSelectionImageTrans[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                towerSpotTouch.touchPos.x,
                towerSpotTouch.touchPos.y
            );
        }
    }

	bool canBuildTower()
	{
		//return GoldManager.gold >= towerSelectPanel.selectedTower.GetComponent<TowerData> ().cost;
		return true;
	}

    bool isInsideSelectionPanel()
    {
        bool result = false;
        Vector3 dis0 = towerSelectionImageTrans[0].GetComponent<RectTransform>().position - Input.mousePosition;
        for(int i = 0; i < 5; i++)
        {
            if ((towerSelectionImageTrans[i].GetComponent<RectTransform>().position - Input.mousePosition).magnitude < 30) result = true;
        }

        return result;
    }
}
