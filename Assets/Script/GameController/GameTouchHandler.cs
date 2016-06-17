using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameTouchHandler : MonoBehaviour {

	[HideInInspector]
	public bool isTowerUpgradeConfirm;

	[HideInInspector]
	public bool isTowerSoldConfirm;

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

	private GraphicRaycaster gr;
	private List<RaycastResult> results;

	void Start () {
	}

	int getGraphicRaycastResultCount(string canvasTag)
	{
		GameObject canvas = GameObject.FindWithTag (canvasTag);
		if (canvas) {
			gr = canvas.GetComponent<GraphicRaycaster> ();
			PointerEventData ped = new PointerEventData (null);
			results = new List<RaycastResult> ();
			ped.position = Input.mousePosition;
			gr.Raycast (ped, results);
			return results.Count;
		} else
			return -1;
	}

	void Update () {
		//if (Input.touchCount == 1) 
		if (Input.GetMouseButtonDown (0)) {
			
			if (getGraphicRaycastResultCount("TUCanvas") >= 1) {
				towerEvolve (results[0].gameObject.tag);
				return;
			}

			if (getGraphicRaycastResultCount("TSCanvas") >= 1) {
				towerSelection (results[0].gameObject.tag);
				return;
			}

			//Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (camRay, out hit, camRayLength)) {
				//Debug.Log ("GameTouchHandler.cs the hit tag is:" + hit.collider.tag);
				switch (hit.collider.tag) {
//				case "TowerUpgrade":
//					isTowerUpgradeConfirm = true;
//					break;
				case "TowerBody":
					isTowerBodyTapped = true;
					break;
//				case "EarthTower":
//					isEarthTowerSelected = true;
//					break;
//				case "FireTower":
//					isFireTowerSelected = true;
//					break;
//				case "WaterTower":
//					isWaterTowerSelected = true;
//					break;
//				case "MetalTower":
//					isMetalTowerSelected = true;
//					break;
//				case "WoodTower":
//					isWoodTowerSelected = true;
//					break;
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
			isTowerSoldConfirm = false;
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

	// Event handle for UI (Tower Selection panel)
	void towerSelection(string towerType)
	{
		switch (towerType) {
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

	public void towerEvolve(string option)
	{
		switch (option) {
		case "TowerUpgrade":
			isTowerUpgradeConfirm = true;
			break;
		case "TowerSold":
			isTowerSoldConfirm = true;
			break;
		default:
			isGameEnvironmentTapped = true;
			break;
		}
	}
}
