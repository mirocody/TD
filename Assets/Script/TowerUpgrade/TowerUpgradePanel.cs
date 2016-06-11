using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerUpgradePanel : MonoBehaviour {

	public float costTxtPosXOffset;
	public float costTxtPosYOffset;

	[HideInInspector]
	public bool isTowerUpgradeConfirm;

	private Camera myCamera;
	private Transform towerUpgradeImageTrans;
	private TowerBodyTouch towerBodyTouch;
	private Transform costTxt;
	private Transform towerUpgradeBtn;
	private TowerUpgradeController towerUpgradeController;


	void Start()
	{
		towerUpgradeImageTrans = transform.Find("TUCanvas/TUImage");
		costTxt = transform.Find ("TUCanvas/TUImage/CostTxt");
		towerUpgradeBtn = transform.Find("TUCanvas/TUImage/TowerUpgradeBtn");
		towerUpgradeController = GameObject.Find ("TowerUpgradeController").GetComponent<TowerUpgradeController> ();
		towerBodyTouch = GameObject.Find("TowerUpgradeController").GetComponent<TowerBodyTouch> ();
		myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();

	}

	void Update()
	{
		scaleTowerUpgradePanel2Camera ();
		anchorTowerUpgradePanel2Tower ();
		// set the upgrade cost
		costTxt.GetComponent<Text> ().text = towerUpgradeController.upgradeCost.ToString();
		if (towerUpgradeController.canUpgradeTower ()) {
			// Set TowerUpgradeBtn interactable
			towerUpgradeBtn.GetComponent<Button> ().interactable = true;
		} else {
			// otherwise disable it
			towerUpgradeBtn.GetComponent<Button> ().interactable = false;
		}
	}

	void scaleTowerUpgradePanel2Camera()
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
			towerUpgradeImageTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
			//costTxt.GetComponent<RectTransform> ().localScale = new Vector3 (newScale, newScale, 1.0f);
		} else {
			// else the camera is perspective...
			cameraSize = myCamera.fieldOfView;
			// if linear regression
			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
			// if Logarithmic regression
			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
			towerUpgradeImageTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
			//costTxt.GetComponent<RectTransform> ().localScale = new Vector3 (newScale, newScale, 1.0f);
		}
	}

	void anchorTowerUpgradePanel2Tower()
	{
		// move the tower selection panel to the touch point
		towerUpgradeImageTrans.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).x,
			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).y
		);
//		costTxt.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
//			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).x + costTxtPosXOffset,
//			myCamera.WorldToScreenPoint(towerBodyTouch.hit.point).y + costTxtPosYOffset 
//		);
	}

	public void TowerUpgrade() {
		isTowerUpgradeConfirm = true;
		Debug.Log ("Tower Upgrade clicked!");
	}

	public void CancelTowerUpgrade()
	{
		gameObject.SetActive (false);
	}

}
