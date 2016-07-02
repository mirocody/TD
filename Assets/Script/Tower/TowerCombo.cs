using UnityEngine;
using System.Collections;

public class TowerCombo : MonoBehaviour {

	public GameObject EarthMetalTower;
	public GameObject FireEarthTower;
	public GameObject WaterWoodTower;
	public GameObject MetalWaterTower;
	public GameObject WoodFireTower;

	[HideInInspector]
	public bool isTowerComboMode;

	GameTouchHandler gameTouch;
	GameObject[] towerObjs;
	TowerRender towerRender;
	RaycastHit myHit;
	char candidateTowerType;

	// Use this for initialization
	void Start () {
		gameTouch = GameObject.Find ("GameTouch").GetComponent<GameTouchHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameTouch.isEarthElementSelected) {
			candidateTowerType = 'g';
			TowerComboInit (candidateTowerType);
		}

		if (gameTouch.isFireElementSelected) {
			candidateTowerType = 'e';
			TowerComboInit ('e');
		}

		if (gameTouch.isWaterElementSelected) {
			candidateTowerType = 'm';
			TowerComboInit ('m');
		}

		if (gameTouch.isMetalElementSelected) {
			candidateTowerType = 'w';
			TowerComboInit ('w');
		}

		if (gameTouch.isWoodElementSelected) {
			candidateTowerType = 'f';
			TowerComboInit ('f');
		}

		if (isTowerComboMode /*&& !TowerCombinationCanceled()*/)
			TowerCombination (candidateTowerType);

		if (TowerCombinationCanceled())
			TowerComboExit ();
	}

	void TowerComboInit(char candidateTowerType)
	{
		isTowerComboMode = true;
		towerObjs = GameObject.FindGameObjectsWithTag ("TowerBody");
		foreach (GameObject towerObj in towerObjs) {
			if (towerObj.GetComponent<TowerData> ().towerType == candidateTowerType && towerObj.GetComponent<TowerData> ().level >= 3) {
				// highlight the towers which are eligible to combine
				towerObj.GetComponent<TowerRender>().isTowerToBeHighlight = true;
			} else {
				towerObj.GetComponent<TowerRender>().isTowerToBeInactive = true;
			}
		}
	}

	bool TowerCombinationCanceled ()
	{
		if(gameTouch.isEarthElementCanceled || gameTouch.isFireElementCanceled || gameTouch.isWaterElementCanceled
		|| gameTouch.isMetalElementCanceled || gameTouch.isWoodElementCanceled) {
			return true;
		}
		return false;
	}

	void TowerCombination (char candidateTowerType)
	{
		if (gameTouch.isTowerBodyTapped) {
			myHit = gameTouch.hit;
			if (myHit.transform.GetComponent<TowerData> ().towerType == candidateTowerType
			    && myHit.transform.GetComponent<TowerData> ().level >= 3) {

				Destroy (myHit.transform.gameObject);

				GameObject comboTower;
				switch (candidateTowerType) {
					case 'g':
						comboTower = (GameObject)Instantiate (
							EarthMetalTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
						TowerComboExit ();
						break;
					case 'm':
						comboTower = (GameObject)Instantiate (
							WaterWoodTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
						TowerComboExit ();
						break;
					case 'w':
						comboTower = (GameObject)Instantiate (
							MetalWaterTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
						TowerComboExit ();
						break;
					case 'f':
						comboTower = (GameObject)Instantiate (
							WoodFireTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
						TowerComboExit ();
						break;
					case 'e':
						comboTower = (GameObject)Instantiate (
							FireEarthTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
						TowerComboExit ();
						break;
					default:
						TowerComboExit ();
						Debug.Log ("Tower type not found!");
						break;
				}
			}
		}
	}

	void TowerComboExit()
	{
		isTowerComboMode = false;
		candidateTowerType = '\0';
		towerObjs = GameObject.FindGameObjectsWithTag ("TowerBody");
		foreach (GameObject towerObj in towerObjs) {
			towerObj.GetComponent<TowerRender> ().isTowerToBeRestored = true;
		}
	}
}