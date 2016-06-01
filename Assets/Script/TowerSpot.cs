using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {
	public GameObject tower;

	void OnMouseUp() {
		Debug.Log("TowerSpot clicked.");
		Instantiate(tower, transform.parent.position, transform.parent.rotation);

		BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
		if(bm.selectedTower != null) {
			GoldManager sm = GameObject.FindObjectOfType<GoldManager>();
			if(sm.gold < bm.selectedTower.GetComponent<Tower>().cost) {
				Debug.Log("Not enough money!");
				return;
			}

			sm.gold -= bm.selectedTower.GetComponent<Tower>().cost;

			// FIXME: Right now we assume that we're an object nested in a parent.
			//GameObject temp = GameObject.FindObjectOfType<Tower>();
			Instantiate(tower, transform.parent.position, transform.parent.rotation);
			Destroy(transform.parent.gameObject);
		}
	}

}
