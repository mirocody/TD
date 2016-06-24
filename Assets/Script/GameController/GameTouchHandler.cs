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
	public RaycastHit hit;

	private float camRayLength = 100f;
    private GraphicRaycaster HUDRayCaster;
    private GraphicRaycaster selectionRayCaster;
    private GraphicRaycaster gr;
    PointerEventData ped;
    List<RaycastResult> results;


    void Start () {
        HUDRayCaster = GameObject.Find("HUD").GetComponent<GraphicRaycaster>();
    }
	
	void Update () {
        //if (Input.touchCount == 1) 
		if (Input.GetMouseButtonDown (0)) {
            //Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

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

            if (Physics.Raycast (camRay, out hit, camRayLength)) {
                //Debug.Log ("GameTouchHandler.cs the hit tag is:" + hit.collider.tag);
                Debug.Log(Time.deltaTime.ToString());
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
            isPauseTapped = false;
            isCloseTapped = false;
            isRestartTapped = false;
            isQuitTapped = false;
            isTowerSoldConfirm = false;
            isCheckBoxTapped = false;
            isMenuButtonTapped = false;
			isGameEnvironmentTapped = false;
		}
	}

    int getGraphicRaycastResultCount(string canvasTag)
    {
        GameObject canvas = GameObject.FindWithTag(canvasTag);
        if (canvas)
        {
            gr = canvas.GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            results = new List<RaycastResult>();
            ped.position = Input.mousePosition;
            gr.Raycast(ped, results);
            return results.Count;
        }
        else
            return -1;
    }

}
