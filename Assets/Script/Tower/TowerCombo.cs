using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerCombo : MonoBehaviour {

	public GameObject EarthMetalTower;
	public GameObject FireEarthTower;
	public GameObject WaterWoodTower;
	public GameObject MetalWaterTower;
	public GameObject WoodFireTower;
    public GameObject[] elementCard=new GameObject[5];

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
            if (InitialData.earthCardNum > 0) { TowerComboInit(candidateTowerType); changeElementCardPanel(0); }
		}

		else if (gameTouch.isFireElementSelected) {
			candidateTowerType = 'e';
			if (InitialData.fireCardNum > 0) { TowerComboInit(candidateTowerType); changeElementCardPanel(1); }
		}

		else if (gameTouch.isWaterElementSelected) {
			candidateTowerType = 'm';
			if (InitialData.waterCardNum > 0) { TowerComboInit(candidateTowerType); changeElementCardPanel(2); }
		}

		else if (gameTouch.isMetalElementSelected) {
			candidateTowerType = 'w';
			if (InitialData.metalCardNum > 0) { TowerComboInit(candidateTowerType); changeElementCardPanel(3); }
		}

		else if (gameTouch.isWoodElementSelected) {
			candidateTowerType = 'f';
			if (InitialData.woodCardNum > 0) { TowerComboInit(candidateTowerType); changeElementCardPanel(4); }
		}

        else if (TowerCombinationCanceled())
            TowerComboExit();

        if (isTowerComboMode)
			TowerCombination (candidateTowerType);

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
        /*if(gameTouch.isEarthElementCanceled || gameTouch.isFireElementCanceled || gameTouch.isWaterElementCanceled
		|| gameTouch.isMetalElementCanceled || gameTouch.isWoodElementCanceled) {
			return true;
		}
		return false;*/
		if (isTowerComboMode && gameTouch.isScreenTouched && !gameTouch.isTowerBodyTapped) return true;
		else if (isTowerComboMode && gameTouch.isScreenTouched && gameTouch.isTowerBodyTapped)
        {
            myHit = gameTouch.hit;
            if (myHit.transform.GetComponent<TowerData>().towerType == candidateTowerType
                && myHit.transform.GetComponent<TowerData>().level >= 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else return false;
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
                        InitialData.earthCardNum--;
                        elementCard[0].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.earthCardNum.ToString();
                        TowerComboExit ();
						break;
					case 'm':
						comboTower = (GameObject)Instantiate (
							WaterWoodTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
                        InitialData.waterCardNum--;
                        elementCard[2].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.waterCardNum.ToString();
                        TowerComboExit ();
						break;
					case 'w':
						comboTower = (GameObject)Instantiate (
							MetalWaterTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
                        InitialData.metalCardNum--;
                        elementCard[3].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.metalCardNum.ToString();
                        TowerComboExit ();
						break;
					case 'f':
						comboTower = (GameObject)Instantiate (
							WoodFireTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
                        InitialData.woodCardNum--;
                        elementCard[4].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.woodCardNum.ToString();
                        TowerComboExit ();
						break;
					case 'e':
						comboTower = (GameObject)Instantiate (
							FireEarthTower,
							myHit.transform.position,
							myHit.transform.rotation
						);
                        InitialData.fireCardNum--;
                        elementCard[1].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.fireCardNum.ToString();
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

		//Fix bug - tower update panel appears after tower combiniation completes
		gameTouch.isTowerBodyTapped = false;

		candidateTowerType = '\0';
		towerObjs = GameObject.FindGameObjectsWithTag ("TowerBody");
		foreach (GameObject towerObj in towerObjs) {
			towerObj.GetComponent<TowerRender> ().isTowerToBeRestored = true;
		}
        for(int i = 0; i < 5; i++)
        {
            string tempString = elementCard[i].transform.GetChild(3).GetChild(0).GetComponent<Text>().text.Trim();
            elementCard[i].transform.GetChild(0).gameObject.SetActive(false);
            if (tempString != "0")
            {
                Color tempColor = elementCard[i].transform.GetChild(1).GetComponent<Image>().color;
                elementCard[i].transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                elementCard[i].transform.GetChild(2).gameObject.SetActive(false);
                elementCard[i].transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                Color tempColor = elementCard[i].transform.GetChild(1).GetComponent<Image>().color;
                elementCard[i].transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 0.6f);
                elementCard[i].transform.GetChild(2).gameObject.SetActive(false);
                elementCard[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
	}

    void changeElementCardPanel(int pos)
    {
        for(int i = 0; i < 5; i++)
        {
            string tempString = elementCard[i].transform.GetChild(3).GetChild(0).GetComponent<Text>().text.Trim();
            if (i == pos)
            {
                //Color tempColor = elementCard[pos].transform.GetChild(1).GetComponent<Image>().color;
                //elementCard[pos].transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                elementCard[i].transform.GetChild(2).gameObject.SetActive(true);
                elementCard[i].transform.GetChild(3).gameObject.SetActive(false);
                elementCard[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                elementCard[i].transform.GetChild(2).gameObject.SetActive(false);
                if(tempString!="0") elementCard[i].transform.GetChild(3).gameObject.SetActive(true);
                elementCard[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

}