using UnityEngine;
using System.Collections;

public class TowerBodyTouch : MonoBehaviour {

	public string layerMaskName;

	[HideInInspector]
	public bool isTowerBodyTapped;

	[HideInInspector]
	public RaycastHit hit;

	private int layerMask;
	private int layerMaskUI;
	private float camRayLength = 100f;

	void Start()
	{
		layerMask = LayerMask.GetMask (layerMaskName);
		layerMaskUI = LayerMask.GetMask ("UI");
	}

	void Update()
	{
		//RaycastHit hit;	

		if (Input.touchCount == 1) {
			Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			if (Physics.Raycast(camRay, out hit, camRayLength, layerMaskUI)) {
				// Return if raycast hits UI layer 
				return;	
			}
			if (Physics.Raycast (camRay, out hit, camRayLength, layerMask)) {
				// Get the hit if a tower body was tapped
				//if (towerBodyTag == hit.collider.tag) {
				isTowerBodyTapped = true;
				Debug.Log ("In TowerBodyTouch.cs, the hitColliderName is: " + hit.collider.name);
				return;
				//}
			}
		}
		isTowerBodyTapped = false;
	}
}
