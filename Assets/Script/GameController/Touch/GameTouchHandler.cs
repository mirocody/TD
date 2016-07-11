using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
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
    public bool isPauseTapped;

    [HideInInspector]
    public bool isCloseTapped;

    [HideInInspector]
    public bool isRestartTapped;

    [HideInInspector]
    public bool isQuitTapped;

    [HideInInspector]
    public bool isTowerSoldConfirm;

    [HideInInspector]
    public bool isCheckBoxTapped;

    [HideInInspector]
    public bool isMenuButtonTapped;

    [HideInInspector]
    public bool isEnemyTapped;

	[HideInInspector]
	public bool isEarthElementSelected;

	[HideInInspector]
	public bool isFireElementSelected;

	[HideInInspector]
	public bool isWaterElementSelected;

	[HideInInspector]
	public bool isMetalElementSelected;

	[HideInInspector]
	public bool isWoodElementSelected;

	[HideInInspector]
	public bool isEarthElementCanceled;

	[HideInInspector]
	public bool isFireElementCanceled;

	[HideInInspector]
	public bool isWaterElementCanceled;

	[HideInInspector]
	public bool isMetalElementCanceled;

	[HideInInspector]
	public bool isWoodElementCanceled;

    [HideInInspector]
    public bool isElementCardTapped;

    [HideInInspector]
    public bool isScreenTouched;

    [HideInInspector]
	public RaycastHit hit;

	private float camRayLength = 100f;
    private GraphicRaycaster gr;
    PointerEventData ped;
    List<RaycastResult> results;
		
	void Update () {

		if (results != null) {
			results.Clear ();
		}

        //if (Input.touchCount == 1) 
		if (Input.GetMouseButtonDown (0)) {
            isScreenTouched = true;
            //Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			//Check whether any HUD component is clicked.
			if (getGraphicRaycastResultCount("HUD") > 0)
			{
				Debug.Log(results[0].gameObject.name);
				switch (results[0].gameObject.tag)
				{
				case "Pause":
					isPauseTapped = true;
					return;
				case "Close":
					isCloseTapped = true;
					return;
				case "Restart":
					isRestartTapped = true;
					return;
				case "Quit":
					isQuitTapped = true;
					return;
				case "CheckBox":
					isCheckBoxTapped = true;
					return;
				case "CheckMark":
					isCheckBoxTapped = true;
					return;
				case "MenuButton":
					isMenuButtonTapped = true;
					return;
				default:
					break;
				}
			}

			//check Instant Transfer Canvas
			if (getGraphicRaycastResultCount ("ITCanvas") > 0) {
				switch (results[0].gameObject.tag)
				{
					case "InstantTransfer":
						results [0].gameObject.GetComponent<InstantTransferPanel> ().isInstantTransferTriggered = true;
						return;
					default:
						break;
				}
			}

			//check Tower Copy Canvas
			if (getGraphicRaycastResultCount ("TCCanvas") > 0) {
				switch (results[0].gameObject.tag)
				{
					case "TowerCopy":
						results [0].gameObject.GetComponent<TowerCopyPanel> ().isTowerCopyTriggered = true;
						return;
					default:
						break;
				}
			}

			//Check Update Canvas.
			if (getGraphicRaycastResultCount("TUCanvas") >= 1)
			{
				switch (results[0].gameObject.tag)
				{
				case "TowerUpgrade":
					isTowerUpgradeConfirm = true;
					return;
				case "TowerSold":
					isTowerSoldConfirm = true;
					return;
				default:
					break;
				}
			}

            //Check whether selectionPanel is clicked.
            if (getGraphicRaycastResultCount("TSCanvas") > 0)
            {
                switch (results[0].gameObject.tag)
                {
                    case "EarthTower":
                        isEarthTowerSelected = true;
                        return;
                    case "FireTower":
                        isFireTowerSelected = true;
                        return;
                    case "WaterTower":
                        isWaterTowerSelected = true;
                        return;
                    case "MetalTower":
                        isMetalTowerSelected = true;
                        return;
                    case "WoodTower":
                        isWoodTowerSelected = true;
                        return;
                    default:
                        break;
                }
            }

			//Check whether Elements Container Panel is clicked.
			if (getGraphicRaycastResultCount("ECCanvas") > 0)
			{
				switch (results[0].gameObject.tag)
				{
				case "EarthElement":
					isEarthElementSelected = true;
					return;
				case "FireElement":
					isFireElementSelected = true;
					return;
				case "WaterElement":
					isWaterElementSelected = true;
					return;
				case "MetalElement":
					isMetalElementSelected = true;
					return;
				case "WoodElement":
					isWoodElementSelected = true;
					return;
				case "EarthElementCancel":
					isEarthElementCanceled = true;
					Debug.Log ("EarthElement Cancel is tapped!");
					return;
				case "FireElementCancel":
					isFireElementCanceled = true;
					return;
				case "WaterElementCancel":
					isWaterElementCanceled = true;
					return;
				case "MetalElementCancel":
					isMetalElementCanceled = true;
					return;
				case "WoodElementCancel":
					isWoodElementCanceled = true;
					return;
				default:
					break;
				}
			}

            if (Physics.Raycast (camRay, out hit, camRayLength)) {
				switch (hit.collider.tag) {
                    case "TowerBody":
                         isTowerBodyTapped = true;
                         break;
				    case "TowerUpgrade":
					    isTowerUpgradeConfirm = true;
					    break;
				    case "TowerSpot":
					    isTowerSpotTapped = true;
					    break;
                    case "Enemy":
                        isEnemyTapped = true;
                        break;
                    case "StaticEnemy":
                        Debug.Log("SE");
                        isEnemyTapped = true;
                        break;
                    case "ElementCard":
                        isElementCardTapped = true;
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
            isScreenTouched = false;
			isTowerUpgradeConfirm = false;
			isTowerBodyTapped = false;
			isEarthTowerSelected = false;
			isFireTowerSelected = false;
			isWaterTowerSelected = false;
			isMetalTowerSelected = false;
			isWoodTowerSelected = false;
			isEarthElementSelected = false;
			isFireElementSelected = false;
			isWaterElementSelected = false;
			isMetalElementSelected = false;
			isWoodElementSelected = false;
			isEarthElementCanceled = false;
			isFireElementCanceled = false;
			isWaterElementCanceled = false;
			isMetalElementCanceled = false;
			isWoodElementCanceled = false;
			isTowerSpotTapped = false;
            isPauseTapped = false;
            isCloseTapped = false;
            isRestartTapped = false;
            isQuitTapped = false;
            isTowerSoldConfirm = false;
            isCheckBoxTapped = false;
            isMenuButtonTapped = false;
            isEnemyTapped = false;
            isElementCardTapped = false;
			isGameEnvironmentTapped = false;
			if (results != null && results.Count > 0)
				results [0].gameObject.GetComponent<InstantTransferPanel> ().isInstantTransferTriggered = false;
			if (results != null && results.Count > 0)
				results [0].gameObject.GetComponent<TowerCopyPanel> ().isTowerCopyTriggered = false;
		}
	}

	int getGraphicRaycastResultCount(string canvasTag)
	{
		GameObject[] multiCanvas = GameObject.FindGameObjectsWithTag (canvasTag);
		if (multiCanvas.Length > 0) {
			foreach (GameObject canvas in multiCanvas) {
				gr = canvas.GetComponent<GraphicRaycaster> ();
				PointerEventData ped = new PointerEventData (null);
				results = new List<RaycastResult> ();
				ped.position = Input.mousePosition;
				gr.Raycast (ped, results);
				if (results.Count > 0)
					return results.Count;
			}
		}
		return -1;
	}

}
