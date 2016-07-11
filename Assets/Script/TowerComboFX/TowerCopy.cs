using UnityEngine;
using System;
using System.Collections;

public class TowerCopy : MonoBehaviour {

	public GameObject copyTower;
	public float towerCopyCD = 10.0f;
	public float yoffset;
	public float outlineImageOffset = -60f;
	public GameObject towerCopyPanel;

	float towerCopyCDRemaining;
	GameObject myTowerCopyPanel;
	Collider[] colliders;

	TowerBuildController tBController;
	bool towerCopyMode;
	int towerCost;
	int copyTowerNum = 0;
	GameObject lastCopied = null;
	Collider lastSpot = null;

	// Use this for initialization
	void Start () {
		tBController = GameObject.Find ("TowerBuild").GetComponent<TowerBuildController> ();
		towerCopyCDRemaining = towerCopyCD;
		towerCost = copyTower.GetComponent<TowerData> ().cost;
	}

	// Update is called once per frame
	void Update () {
		// instantiate instant transfer panel every 'intantTransferCDRemaining' seconds
		if (myTowerCopyPanel == null)
			towerCopyCDRemaining -= Time.deltaTime;
		if (towerCopyCDRemaining <= 0) {
			if(lastCopied != null) {
				Destroy (lastCopied);
				tBController.occupiedTowerSpots.Remove(lastSpot.name);
			}

			myTowerCopyPanel = (GameObject)Instantiate (towerCopyPanel);
			myTowerCopyPanel.transform.Find ("TCCanvas/TCImage").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
				Camera.main.WorldToScreenPoint (transform.position).x,
				Camera.main.WorldToScreenPoint (transform.position).y + yoffset
			);
			myTowerCopyPanel.transform.Find ("TCCanvas/OutlineImage").GetComponent<RectTransform> ().anchoredPosition = 
				myTowerCopyPanel.transform.Find ("TCCanvas/TCImage").GetComponent<RectTransform> ().anchoredPosition +
				new Vector2 (0, outlineImageOffset);
			towerCopyCDRemaining = towerCopyCD;
		}

		// Destroy the ITPanel after player clicked on it
		if (myTowerCopyPanel) {
			if (myTowerCopyPanel.transform.Find ("TCCanvas/TCImage").GetComponent<TowerCopyPanel> ().isTowerCopyTriggered) {
				Destroy (myTowerCopyPanel);

				towerCopyMode = true;
			}
		}

		// Set related enemies to instant transfer mode, run only one time each cycle
		if (towerCopyMode) {
			towerCopyMode = false;

			//myHit = gameTouch.hit;

			Collider[] colliders;
			colliders = Physics.OverlapSphere(transform.position, 10.0f);


			if(colliders.Length > 1)
			{

				foreach (Collider c in colliders) {

					if (c.tag=="TowerBody") {

						GameObject thisTower = c.transform.gameObject;
						int thisCost = thisTower.GetComponent<TowerData> ().cost;
						int thisLevel = thisTower.GetComponent<TowerData> ().level;

						if(thisLevel<4 && thisCost > towerCost) {
							copyTower = thisTower;
							towerCost = thisCost;
						}
							
					}

				}

				int spotNum = 0;

				Collider thisSpot = colliders[0];
				float dist = Vector3.Distance(thisSpot.transform.position, transform.position);
				float dist2 = 0f;

				foreach (Collider c in colliders) {
					
					if(spotNum==0  && c.tag=="TowerSpot" && !tBController.occupiedTowerSpots.Contains(c.name)
						&& Vector3.Distance(c.transform.position, transform.position)>0) {

						thisSpot = c;
						dist = Vector3.Distance(thisSpot.transform.position, transform.position);
						spotNum++;
					}

					dist2 = Vector3.Distance(c.transform.position, transform.position);

					if (spotNum>0 && c.tag=="TowerSpot" && !tBController.occupiedTowerSpots.Contains(c.name) && dist2>0 && dist2<dist) {

						thisSpot = c;
						dist = dist2;
						spotNum++;
					}

				}

				if(spotNum>0) {
					GameObject myTower=(GameObject)Instantiate (copyTower, thisSpot.transform.position, transform.rotation);
					tBController.occupiedTowerSpots.Add (thisSpot.name);
					lastCopied = myTower;
					lastSpot = thisSpot;
				}

			}
		}
	}
}
