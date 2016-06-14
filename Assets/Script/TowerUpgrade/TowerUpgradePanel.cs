using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerUpgradePanel : MonoBehaviour {

	public float yoffset;

//	[HideInInspector]
//	public bool isTowerUpgradeConfirm;

	//private Camera myCamera;
	//private Transform towerUpgradeContainerTrans;
	//private TowerBodyTouch towerBodyTouch;
	private Transform costTxt;
	private Transform towerUpgradeImg;
	private TowerUpgradeController towerUpgradeController;


	void Start()
	{
		//towerUpgradeContainerTrans = transform.Find("TUCanvas/TUContainer");
		costTxt = transform.Find ("TUCanvas/TUContainer/CostTxt");
		towerUpgradeImg = transform.Find("TUCanvas/TUContainer/TowerUpgradeImage");
		towerUpgradeController = GameObject.Find ("TowerUpgrade").GetComponent<TowerUpgradeController> ();
		//towerBodyTouch = GameObject.Find("TowerUpgrade").GetComponent<TowerBodyTouch> ();
		//myCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	void Update()
	{
		//scaleTowerUpgradePanel2Camera ();
		//anchorTowerUpgradePanel2Tower ();
		// set the upgrade cost
		costTxt.GetComponent<Text> ().text = towerUpgradeController.upgradeCost.ToString();
		if (towerUpgradeController.canUpgradeTower ()) {
			// Set TowerUpgradeBtn interactable
			//towerUpgradeBtn.GetComponent<Button> ().interactable = true;
			towerUpgradeImg.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		} else {
			// otherwise disable it
			//towerUpgradeBtn.GetComponent<Button> ().interactable = false;
			towerUpgradeImg.GetComponent<Image>().color = new Color (1f, 1f, 1f, 0.5f);
		}
	}

//	void scaleTowerUpgradePanel2Camera()
//	{
//		float cameraSize;
//		float newScale;
//
//		// change the scale of tower selection panel, in relation to the FOV or size of camera, and also screen
//		if (myCamera.orthographic) {
//			// If the camera is orthographic...
//			cameraSize = myCamera.orthographicSize;
//			// if linear regression
//			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
//			// if Logarithmic regression
//			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
//			towerUpgradeContainerTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
//			//costTxt.GetComponent<RectTransform> ().localScale = new Vector3 (newScale, newScale, 1.0f);
//		} else {
//			// else the camera is perspective...
//			cameraSize = myCamera.fieldOfView;
//			// if linear regression
//			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
//			// if Logarithmic regression
//			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
//			towerUpgradeContainerTrans.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
//			//costTxt.GetComponent<RectTransform> ().localScale = new Vector3 (newScale, newScale, 1.0f);
//		}
//	}
}
