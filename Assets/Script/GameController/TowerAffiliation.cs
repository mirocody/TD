using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAffiliation : MonoBehaviour {

	public GameObject explosionPrefab;
	public string layMaskName;
	public string hitColliderName;

	private GameObject[] towers;
	private List<Vector3> towerMembersPos = new List<Vector3>();
	private int layerMask;
	private float camRayLength = 100f;

	// Use this for initialization
	void Start () {
		layerMask = LayerMask.GetMask (layMaskName);
	}

	void Update()
	{
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit hit;

		if (Input.touchCount > 1) {
			for (int i = 0; i < Input.touchCount; i++) {
				Ray camRay = Camera.main.ScreenPointToRay (Input.GetTouch (i).position);
				if (Physics.Raycast (camRay, out hit, camRayLength, layerMask)) {
					if (hit.collider.name == hitColliderName) {
						towerMembersPos.Add (hit.point);
					} else {
						// return if hit something else other than tower body in specified layer
						return;
					}
				} else {
					// return if hit nothing in specified layer
					return;
				}
			}

			float xSum = 0, ySum = 0, zSum = 0;

			for (int i = 0; i < towerMembersPos.Count; i++) {
				xSum += towerMembersPos [i].x;
				ySum += towerMembersPos [i].y;
				zSum += towerMembersPos [i].z;
			}

			if (towerMembersPos.Count > 0) {
				AdvancedAttack (new Vector3 (
					xSum / towerMembersPos.Count,
					0,
					zSum / towerMembersPos.Count)
				);
			}
		}
	}

	void AdvancedAttack(Vector3 attackSpot)
	{
		Instantiate(explosionPrefab, attackSpot, Quaternion.identity);

		towerMembersPos.RemoveRange (0, towerMembersPos.Count);
	}
}

