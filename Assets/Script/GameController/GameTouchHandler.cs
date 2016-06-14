using UnityEngine;
using System.Collections;

public class GameTouchHandler : MonoBehaviour {

	[HideInInspector]
	public bool isTowerUpgradeConfirm;

	[HideInInspector]
	public bool isTowerBodyTapped;

	[HideInInspector]
	public bool isEarthTowerSelected;

	[HideInInspector]
	public bool isFireTowerSelected;

	[HideInInspector]
	public bool isWaterTowerSelected;

	[HideInInspector]
	public bool isMetalTowerSelected;

	[HideInInspector]
	public bool isWoodTowerSelected;

	[HideInInspector]
	public bool isTowerSpotTapped;

	[HideInInspector]
	public bool isGameEnvironmentTapped;

	[HideInInspector]
	public RaycastHit hit;

	private float camRayLength = 100f;

	void Start () {

	}
	
	void Update () {
		//if (Input.touchCount == 1) 
		if (Input.GetMouseButtonDown (0)) {
			//Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (camRay, out hit, camRayLength)) {
				//Debug.Log ("GameTouchHandler.cs the hit tag is:" + hit.collider.tag);
				switch (hit.collider.tag) {
				case "TowerUpgrade":
					isTowerUpgradeConfirm = true;
					break;
				case "TowerBody":
					isTowerBodyTapped = true;
					break;
				case "EarthTower":
					isEarthTowerSelected = true;
					break;
				case "FireTower":
					isFireTowerSelected = true;
					break;
				case "WaterTower":
					isWaterTowerSelected = true;
					break;
				case "MetalTower":
					isMetalTowerSelected = true;
					break;
				case "WoodTower":
					isWoodTowerSelected = true;
					break;
				case "TowerSpot":
					isTowerSpotTapped = true;
					break;
				default:
					isGameEnvironmentTapped = true;
					break;
				}
			} 
			else {
				// if player tapped anything but above game objects
				isGameEnvironmentTapped = true;
			}
		} 
		else {
			// if player did not tap the screen
			isTowerUpgradeConfirm = false;
			isTowerBodyTapped = false;
			isEarthTowerSelected = false;
			isFireTowerSelected = false;
			isWaterTowerSelected = false;
			isMetalTowerSelected = false;
			isWoodTowerSelected = false;
			isTowerSpotTapped = false;
			isGameEnvironmentTapped = false;
		}
	}
}
