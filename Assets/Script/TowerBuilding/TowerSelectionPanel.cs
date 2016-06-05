using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour {

	[HideInInspector]
	public bool isTowerSelected;

	[HideInInspector]
	public GameObject selectedTower;

	private Camera myCamera;
	private Transform towerSelectionImageTrans;
	private TowerSpotTouch towerSpotTouch;

	void Start()
	{
		towerSelectionImageTrans = transform.Find("TSCanvas/TSImage");
		towerSpotTouch = GameObject.Find("TowerBuildController").GetComponent<TowerSpotTouch> ();
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();
	}

	void Update()
	{
		scaleTowerSelectionPanel2Camera ();
		anchorTowerSelectionPanel2TowerSpot ();
	}

	void scaleTowerSelectionPanel2Camera()
	{
		float cameraSize;
		float newScale;

		// change the scale of tower selection panel, in relation to the FOV or size of camera, and also screen
		if (myCamera.orthographic) {
			// If the camera is orthographic...
			cameraSize = myCamera.orthographicSize;
			// if linear regression
			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
			// if Logarithmic regression
			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
			towerSelectionImageTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
		} else {
			// else the camera is perspective...
			cameraSize = myCamera.fieldOfView;
			// if linear regression
			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
			// if Logarithmic regression
			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
			towerSelectionImageTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);	
		}
	}

	void anchorTowerSelectionPanel2TowerSpot()
	{
		
		// move the tower selection panel to the touch point
		towerSelectionImageTrans.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
			myCamera.WorldToScreenPoint(towerSpotTouch.hitPoint).x, //towerSpotTouch.touchPos.x, 
			myCamera.WorldToScreenPoint(towerSpotTouch.hitPoint).y  //towerSpotTouch.touchPos.y
		);
	}

	public void SelectTowerType(GameObject prefab) {
		selectedTower = prefab;
		isTowerSelected = true;
		//Debug.Log ("Tower Selected!");
	}

	public void CancelSelectTower()
	{
		gameObject.SetActive (false);
	}
}
