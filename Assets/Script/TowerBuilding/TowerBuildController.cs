using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildController : MonoBehaviour {

	public GameObject[] towers; 
	public GameObject towerSelectionPanel;
	public float yOffset;
    public List<string> occupiedTowerSpots = new List<string>();

    GameTouchHandler gameTouch;
	RaycastHit myHit;
	GameObject selectedTower;
	GameObject myTowerSelectionPanel;

	void Start () {
		gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler> ();
	}
	
	void Update () {

        if (gameTouch.isTowerSpotTapped && Time.timeScale != 0)
        {
            myHit = gameTouch.hit;
            if (!occupiedTowerSpots.Contains(/*towerSpotTouch*/myHit.collider.name))
            {
                Destroy(myTowerSelectionPanel);
                myTowerSelectionPanel = (GameObject)Instantiate(towerSelectionPanel,
                    new Vector3(0, 0, 0),
                    myHit.transform.rotation
                );
                myTowerSelectionPanel.SetActive(true);
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

        checkHUDController();

		if (gameTouch.isGameEnvironmentTapped) {
			Destroy(myTowerSelectionPanel);
		}
	}

	bool canBuildTower()
	{
		return GoldManager.gold >= selectedTower.GetComponent<TowerData> ().cost;
	}

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
    //Check whether any UI component is clicked
    void checkHUDController()
    {
        if (gameTouch.isPauseTapped)
        {
            HUDController.clickPauseButton();
        }
        if (gameTouch.isCloseTapped)
        {
            HUDController.clickCloseButton();
        }
        if (gameTouch.isRestartTapped)
        {
            HUDController.clickRestartButton();
        }
        if (gameTouch.isQuitTapped)
        {
            HUDController.clickQuitButton();
        }
    }
}
