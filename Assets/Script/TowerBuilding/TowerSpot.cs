using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	GoldManager gm;
	Transform tsPanelTrans;
	TowerSelectionPanel tsPanelScript;
	GameObject tower;

	void Start()
	{
		tsPanelTrans = transform.Find ("TowerSelectionPanel");
		tsPanelScript = tsPanelTrans.GetComponent<TowerSelectionPanel> ();
		gm = GameObject.FindObjectOfType<GoldManager> ();
	}

	void OnMouseUp() {
		Debug.Log("TowerSpot clicked.");
		if (GameObject.Find ("TowerSelectionPanel") == null) {
			tsPanelTrans.gameObject.SetActive (true);
		}
	}

	void Update()
	{
		if (tsPanelScript.selectedTower) {
			tsPanelTrans.gameObject.SetActive (false);
			if (CanBuildTower ()) {
				gm.gold -= tsPanelScript.selectedTower.GetComponent<TowerData> ().cost;
				Debug.Log ("Tower created");
				tower = (GameObject)Instantiate (tsPanelScript.selectedTower, transform.position, transform.rotation);
				//Destroy (transform.parent.gameObject);
			}
			tsPanelScript.selectedTower = null;
		}
	}

	bool CanBuildTower()
	{
		if (tower == null) {
			if (gm.gold >= tsPanelScript.selectedTower.GetComponent<TowerData> ().cost) {
				Debug.Log ("Have enough money!");
				return true;
			}		
		}

		return false;
	}
}
