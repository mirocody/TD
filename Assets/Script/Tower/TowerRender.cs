using UnityEngine;
using System.Collections;

public class TowerRender : MonoBehaviour {

	[HideInInspector]
	public bool isTowerToBeHighlight;

	[HideInInspector]
	public bool isTowerToBeInactive;

	[HideInInspector]
	public bool isTowerToBeRestored;

	TowerData myTowerData;
	TowerCombo towerCombo;
	//string resPath;

	Material baseMat;
	Material turretMat;
	Material barrelMat;
	Material crystalMat;
	Material ringMat;
	Material woodMat;
	Material rockMat;

	// Use this for initialization
	void Start () {
		myTowerData = GetComponent<TowerData> ();
		char myTowerType = myTowerData.towerType;
//		switch (myTowerType) {
//			case 'e':
//				resPath = "Towers/EarthTower/";
//				break;
//			case 'f':
//				resPath = "Towers/FireTower/";
//				break;
//			case 'g':
//				resPath = "Towers/MetalTower/";
//				break;
//			case 'w':
//				resPath = "Towers/WaterTower/";
//				break;
//			case 'm':
//				resPath = "Towers/WoodTower/";
//				break;
//			case 'z':
//				resPath = "Towers/FireEarthTower/";
//				break;
//			default:
//				break;
//		}

		towerCombo = GameObject.Find ("TowerCombo").GetComponent<TowerCombo> ();

		// store the mat before tower combo so that we can restore it when tower combo ends
		if (transform.Find ("Base"))
			baseMat = transform.Find ("Base").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Turret"))
			turretMat = transform.Find ("Tower_Top/Turret").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Turret/Barrel"))
			barrelMat = transform.Find ("Tower_Top/Turret/Barrel").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Crystal"))
			crystalMat = transform.Find ("Tower_Top/Crystal").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Ring"))
			ringMat = transform.Find ("Tower_Top/Ring").GetComponent<Renderer> ().material;

		// special case for fire-wood tower
		if (transform.Find ("Wood"))
			woodMat = transform.Find ("Wood").GetComponent<Renderer> ().material;

		// special case for metal-earth tower
		if (transform.Find ("Rock"))
			rockMat = transform.Find ("Rock").GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTowerToBeHighlight) {
			// highlight the tower if it is eligible

			isTowerToBeHighlight = false;
		}

		if (isTowerToBeInactive) {
			// disable the unqualified tower
			//Material BaseTransMat = (Material)Resources.Load(resPath + "BaseTransMat");
			Material transMat = (Material)Resources.Load("Towers/TransMat");
			if (transform.Find("Base"))
				transform.Find("Base").GetComponent<Renderer>().material = transMat;

			//Material TopTransMat = (Material)Resources.Load(resPath + "TopTransMat");
			if (transform.Find("Tower_Top/Turret"))
				transform.Find("Tower_Top/Turret").GetComponent<Renderer>().material = transMat;

			//Material TopDecoTransMat = (Material)Resources.Load(resPath + "TopDecoTransMat");
			if (transform.Find("Tower_Top/Turret/Barrel"))
				transform.Find("Tower_Top/Turret/Barrel").GetComponent<Renderer>().material = transMat;

			if (transform.Find ("Tower_Top/Crystal"))
				transform.Find ("Tower_Top/Crystal").GetComponent<Renderer> ().material = transMat;

			if (transform.Find ("Tower_Top/Ring"))
				transform.Find ("Tower_Top/Ring").GetComponent<Renderer> ().material = transMat;
			
			if (transform.Find ("Light"))
				transform.Find ("Light").gameObject.SetActive (false);

			// special case for earth-fire tower
			if (transform.Find ("Fire_Effect"))
				transform.Find ("Fire_Effect").gameObject.SetActive (false);

			// special case for fire-wood tower
			if (transform.Find ("Wood")) {
				transform.Find ("Wood").GetComponent<Renderer> ().material = transMat;
			}

			// special case for metal-earth tower
			if (transform.Find ("Rock"))
				transform.Find ("Rock").GetComponent<Renderer> ().material = transMat;

			// special case for water-metal tower
			if (transform.Find ("Flash"))
				transform.Find ("Flash").gameObject.SetActive (false);

			// special case for wood-water tower
			if (transform.Find ("Tesla_glow"))
				transform.Find ("Tesla_glow").gameObject.SetActive (false);

//			if (transform.Find ("Font"))
//				transform.Find ("Font").gameObject.SetActive (false);
//
//			// special case for wood tower
//			if (transform.Find ("bush"))
//				transform.Find ("bush").gameObject.SetActive (false);
//
//			if (transform.Find ("Tree"))
//				transform.Find ("Tree").gameObject.SetActive (false);

			isTowerToBeInactive = false;
		}

		if (isTowerToBeRestored)
		{
			Debug.Log ("TowerRender.cs - Restore tower to original state!");
			// restore the tower render to its original state
			if (transform.Find ("Base"))
				transform.Find ("Base").GetComponent<Renderer> ().material = baseMat;
			if (transform.Find ("Tower_Top/Turret"))
				transform.Find ("Tower_Top/Turret").GetComponent<Renderer> ().material = turretMat;
			if (transform.Find ("Tower_Top/Turret/Barrel"))
				transform.Find ("Tower_Top/Turret/Barrel").GetComponent<Renderer> ().material = barrelMat;
			if (transform.Find ("Tower_Top/Crystal"))
				transform.Find ("Tower_Top/Crystal").GetComponent<Renderer> ().material = crystalMat;
			if (transform.Find ("Tower_Top/Ring"))
				transform.Find ("Tower_Top/Ring").GetComponent<Renderer> ().material = ringMat;
			
			if (transform.Find ("Light"))
				transform.Find ("Light").gameObject.SetActive (true);

			// special case for earth-fire tower
			if (transform.Find ("Fire_Effect"))
				transform.Find ("Fire_Effect").gameObject.SetActive (true);

			// special case for fire-wood tower
			if (transform.Find ("Wood"))
				transform.Find ("Wood").GetComponent<Renderer> ().material = woodMat;

			// special case for metal-earth tower
			if (transform.Find ("Rock"))
				transform.Find ("Rock").GetComponent<Renderer> ().material = rockMat;

			// special case for water-metal tower
			if (transform.Find ("Flash"))
				transform.Find ("Flash").gameObject.SetActive (true);

			// special case for wood-water tower
			if (transform.Find ("Tesla_glow"))
				transform.Find ("Tesla_glow").gameObject.SetActive (true);
			
//			// special case for fire tower
//			if (transform.Find ("Campfire"))
//				transform.Find ("Campfire").gameObject.SetActive (true);
//
//			// special case for water tower
//			if (transform.Find ("Surface Splash"))
//				transform.Find ("Surface Splash").gameObject.SetActive (true);
//
//			if (transform.Find ("Font"))
//				transform.Find ("Font").gameObject.SetActive (true);
//
//			// special case for wood tower
//			if (transform.Find ("bush"))
//				transform.Find ("bush").gameObject.SetActive (true);
//
//			if (transform.Find ("Tree"))
//				transform.Find ("Tree").gameObject.SetActive (true);

			isTowerToBeRestored = false;
		}
	}
}
