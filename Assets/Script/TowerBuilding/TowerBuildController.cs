using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildController : MonoBehaviour {

	public GameObject[] towers;
	public GameObject towerSelectionPanel;
	public float yOffset;
	public int[] costs;

    [HideInInspector]
	public List<string> occupiedTowerSpots = new List<string>();

    GameTouchHandler gameTouch;
	RaycastHit myHit;
	GameObject selectedTower;
	GameObject myTowerSelectionPanel;
	TowerCombo towerCombo;

	void Start () {
		gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler> ();
		towerCombo = GameObject.Find("TowerCombo").GetComponent<TowerCombo>();

		// pass the build cost to TowerSelectionPanel.cs
		costs = new int[towers.Length];
		for (int i = 0; i < towers.Length; i++) {
			towers [i].GetComponent<TowerData> ().init ();
			costs [i] = towers [i].GetComponent<TowerData> ().cost;
		}
	}

	void Update () {

		if (gameTouch.isTowerSpotTapped && !towerCombo.isTowerComboMode)
        {
            myHit = gameTouch.hit;
            if (!occupiedTowerSpots.Contains(myHit.collider.name))
            {
				// Find and Destroy existing upgrade & selection panel(s) before instantiating new ones
				Destroy(myTowerSelectionPanel);

				GameObject[] existingTUPanels = GameObject.FindGameObjectsWithTag("TowerUpgradePanel");
				foreach (GameObject existingTUPanel in existingTUPanels) {
					Destroy (existingTUPanel);
				}

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
				GameObject myTower=(GameObject)Instantiate (selectedTower, myHit.transform.position, myHit.transform.rotation);
				myTower.transform.Rotate(0, 180, 0);
				GoldManager.gold -= myTower.GetComponent<TowerData> ().cost;
				occupiedTowerSpots.Add (myHit.collider.name);
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
			selectedTower.GetComponent<TowerData>().init();

		} else if (gameTouch.isWoodTowerSelected) {
			isSelected = true;
			selectedTower = towers [1];
			selectedTower.GetComponent<TowerData>().init();
		} else if (gameTouch.isMetalTowerSelected) {
			isSelected = true;
			selectedTower = towers [2];
			selectedTower.GetComponent<TowerData>().init();
		} else if (gameTouch.isFireTowerSelected) {
			isSelected = true;
			selectedTower = towers [3];
			selectedTower.GetComponent<TowerData>().init();
		} else if (gameTouch.isWaterTowerSelected) {
			isSelected = true;
			selectedTower = towers [4];
			selectedTower.GetComponent<TowerData>().init();
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
        if (gameTouch.isCheckBoxTapped)
        {
            Debug.Log("ChangeBGM");
            HUDController.changeBGMStatus();
        }
        if (gameTouch.isMenuButtonTapped)
        {
            HUDController.showMenu();
        }
    }
}
