using UnityEngine;
using System.Collections;

public class TowerSpotTouch : MonoBehaviour {

	public string layerMaskName;
	public string towerSpotTag;

	[HideInInspector]
	public string hitColliderName;
	[HideInInspector]
	public Vector3 hitPoint;
	[HideInInspector]
	public Vector2 touchPos;
	[HideInInspector]
	public bool isTowerSpotTapped;
    public bool isBlank;
    public bool isSelectionPanel;

	private int layerMask;
	private int layerMaskUI;
	private float camRayLength = 100f;

	void Start()
	{
        isBlank = true;
        isSelectionPanel = false;
		layerMask = LayerMask.GetMask (layerMaskName);
		layerMaskUI = LayerMask.GetMask ("UI");
	}

	void Update()
	{
		RaycastHit hit;	

		//if (Input.touchCount==1)
        if(Input.GetMouseButtonDown(0))
        {
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(camRay, out hit, camRayLength, layerMaskUI)) {
                // Return if raycast hits UI layer
				return;	
			}
			if (Physics.Raycast (camRay, out hit, camRayLength, layerMask)) {
				// Get collider name, hit point and touch position if a tower spot was tapped
				if (towerSpotTag == hit.collider.tag) {
					isTowerSpotTapped = true;
                    isBlank = false;
					hitColliderName = hit.collider.name;
					hitPoint = hit.point;
					touchPos = Camera.main.WorldToScreenPoint( hit.transform.position);
					Debug.Log ("In TowerSpotTouch.cs, the hitColliderName is: " + hitColliderName);
					return;
				}
			}
            Debug.Log("Did not hit anything!");
            isBlank = true;
        }
        isTowerSpotTapped = false;
		//hitColliderName = "";
	}
}
