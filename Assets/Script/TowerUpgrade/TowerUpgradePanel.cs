using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerUpgradePanel : MonoBehaviour {

	private Transform costTxt;
	private Transform towerUpgradeImg;
	private TowerUpgradeController towerUpgradeController;


	void Start()
	{
		costTxt = transform.Find ("TUCanvas/CostTxt");
		towerUpgradeImg = transform.Find("TUCanvas/TowerUpgradeImage");
		towerUpgradeController = GameObject.Find ("TowerUpgrade").GetComponent<TowerUpgradeController> ();
	}

	void Update()
	{
		//scaleTowerUpgradePanel2Camera ();
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


}
