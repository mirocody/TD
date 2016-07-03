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
//	string resPath;

	Material baseMat;
	Material baseDecoMat;
	Material shieldMat;
	Material topMat;
	Material topDecoMat;

	Material transMat;

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
		if (transform.Find ("Tower_Base/Tower_Base"))
			baseMat = transform.Find ("Tower_Base/Tower_Base").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Base/Tower_Base_Deco"))
			baseDecoMat = transform.Find ("Tower_Base/Tower_Base_Deco").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Base/Shield"))
			shieldMat = transform.Find ("Tower_Base/Shield").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Tower_Top"))
			topMat = transform.Find ("Tower_Top/Tower_Top").GetComponent<Renderer> ().material;
		if (transform.Find ("Tower_Top/Tower_Top_Deco"))
			topDecoMat = transform.Find ("Tower_Top/Tower_Top_Deco").GetComponent<Renderer> ().material;
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

			if (transform.Find("Tower_Base/Tower_Base"))
				//transform.Find("Tower_Base/Tower_Base").GetComponent<Renderer>().material = BaseTransMat;
				transform.Find("Tower_Base/Tower_Base").GetComponent<Renderer>().material = transMat;
			
			//Material BaseDecoTransMat = (Material)Resources.Load(resPath + "BaseDecoTransMat");
			if (transform.Find("Tower_Base/Tower_Base_Deco"))
				//transform.Find("Tower_Base/Tower_Base_Deco").GetComponent<Renderer>().material = BaseDecoTransMat;
				transform.Find("Tower_Base/Tower_Base_Deco").GetComponent<Renderer>().material = transMat;
			
			//Material ShieldTransMat = (Material)Resources.Load(resPath + "ShieldTransMat");
			if (transform.Find("Tower_Base/Shield"))
				//transform.Find("Tower_Base/Shield").GetComponent<Renderer>().material = ShieldTransMat;
				transform.Find("Tower_Base/Shield").GetComponent<Renderer>().material = transMat;
			
			//Material TopTransMat = (Material)Resources.Load(resPath + "TopTransMat");
			if (transform.Find("Tower_Top/Tower_Top"))
				//transform.Find("Tower_Top/Tower_Top").GetComponent<Renderer>().material = TopTransMat;
				transform.Find("Tower_Top/Tower_Top").GetComponent<Renderer>().material = transMat;
			
			//Material TopDecoTransMat = (Material)Resources.Load(resPath + "TopDecoTransMat");
			if (transform.Find("Tower_Top/Tower_Top_Deco"))
				//transform.Find("Tower_Top/Tower_Top_Deco").GetComponent<Renderer>().material = TopDecoTransMat;
				transform.Find("Tower_Top/Tower_Top_Deco").GetComponent<Renderer>().material = transMat;
			
			if (transform.Find ("Light"))
				transform.Find ("Light").gameObject.SetActive (false);

			// special case for fire tower
			if (transform.Find ("Campfire"))
				transform.Find ("Campfire").gameObject.SetActive (false);

			// special case for water tower
			if (transform.Find ("Surface Splash"))
				transform.Find ("Surface Splash").gameObject.SetActive (false);

			if (transform.Find ("Font"))
				transform.Find ("Font").gameObject.SetActive (false);

			// special case for wood tower
			if (transform.Find ("bush"))
				transform.Find ("bush").gameObject.SetActive (false);

			if (transform.Find ("Tree"))
				transform.Find ("Tree").gameObject.SetActive (false);

			isTowerToBeInactive = false;
		}

		if (isTowerToBeRestored)
		{
			Debug.Log ("TowerRender.cs - Restore tower to original state!");
			// restore the tower render to its original state
			if (transform.Find ("Tower_Base/Tower_Base"))
				transform.Find ("Tower_Base/Tower_Base").GetComponent<Renderer> ().material = baseMat;
			if (transform.Find ("Tower_Base/Tower_Base_Deco"))
				transform.Find ("Tower_Base/Tower_Base_Deco").GetComponent<Renderer> ().material = baseDecoMat;
			if (transform.Find ("Tower_Base/Shield"))
				transform.Find ("Tower_Base/Shield").GetComponent<Renderer> ().material = shieldMat;
			if (transform.Find ("Tower_Top/Tower_Top"))
				transform.Find ("Tower_Top/Tower_Top").GetComponent<Renderer> ().material = topMat;
			if (transform.Find ("Tower_Top/Tower_Top_Deco"))
				transform.Find ("Tower_Top/Tower_Top_Deco").GetComponent<Renderer> ().material = topDecoMat;
			if (transform.Find ("Light"))
				transform.Find ("Light").gameObject.SetActive (true);

			// special case for fire tower
			if (transform.Find ("Campfire"))
				transform.Find ("Campfire").gameObject.SetActive (true);

			// special case for water tower
			if (transform.Find ("Surface Splash"))
				transform.Find ("Surface Splash").gameObject.SetActive (true);

			if (transform.Find ("Font"))
				transform.Find ("Font").gameObject.SetActive (true);

			// special case for wood tower
			if (transform.Find ("bush"))
				transform.Find ("bush").gameObject.SetActive (true);

			if (transform.Find ("Tree"))
				transform.Find ("Tree").gameObject.SetActive (true);

			isTowerToBeRestored = false;
		}
	}
}
