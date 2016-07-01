using UnityEngine;
using System;
using System.Collections;

public class InstantTransfer : MonoBehaviour {

	public float intantTransferCD = 10.0f;
	public float yoffset;
	public float outlineImageOffset = -60f;
	public GameObject instantTransferPanel;

	float intantTransferCDRemaining;
	GameObject myInstantTransferPanel;
	Collider[] colliders;
	bool instantTransferMode;

	// Use this for initialization
	void Start () {
		intantTransferCDRemaining = intantTransferCD;
	}
	
	// Update is called once per frame
	void Update () {
		// instantiate instant transfer panel every 'intantTransferCDRemaining' seconds
		if (myInstantTransferPanel == null)
			intantTransferCDRemaining -= Time.deltaTime;
		if (intantTransferCDRemaining <= 0) {
			myInstantTransferPanel = (GameObject)Instantiate (instantTransferPanel);
			myInstantTransferPanel.transform.Find ("ITCanvas/ITImage").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (
				Camera.main.WorldToScreenPoint (transform.position).x,
				Camera.main.WorldToScreenPoint (transform.position).y + yoffset
			);
			myInstantTransferPanel.transform.Find ("ITCanvas/OutlineImage").GetComponent<RectTransform> ().anchoredPosition = 
				myInstantTransferPanel.transform.Find ("ITCanvas/ITImage").GetComponent<RectTransform> ().anchoredPosition +
				new Vector2 (0, outlineImageOffset);
			intantTransferCDRemaining = intantTransferCD;
		}

		// Destroy the ITPanel after player clicked on it
		if (myInstantTransferPanel) {
			if (myInstantTransferPanel.transform.Find ("ITCanvas/ITImage").GetComponent<InstantTransferPanel> ().isInstantTransferTriggered) {
				Destroy (myInstantTransferPanel);

				colliders = Physics.OverlapSphere (transform.position, transform.GetComponent<TowerData> ().range);
				if (colliders.Length > 1) {
					Debug.Log ("instant transfer mode is true!");
					instantTransferMode = true;
				}
			}
		}

		// Set related enemies to instant transfer mode, run only one time each cycle
		if (instantTransferMode) {
			foreach (Collider c in colliders) {
				if (c.tag == "Enemy") {
					c.GetComponent<InstantTransferEnemy> ().instantTransferMode = true;
				}
			}
			instantTransferMode = false;
			Array.Clear (colliders, 0, colliders.Length);
		}
	}
}
