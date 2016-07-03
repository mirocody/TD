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
		if (towerUpgradeController.towerlevel < 3) {
			costTxt.GetComponent<Text> ().text = towerUpgradeController.upgradeCost.ToString ();
			if (GoldManager.gold >= towerUpgradeController.upgradeCost) {
				towerUpgradeImg.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			} else {
				towerUpgradeImg.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.5f);
			}
		} else {
			costTxt.GetComponent<Text> ().text = "";
			towerUpgradeImg.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.5f);
		}
	}
}
