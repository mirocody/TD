using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour {

	public float radius;

	private Transform[] towerSelectionImageTrans;
    private float speed = 1f;
    private float distance = 0;

	void Start()
	{
        towerSelectionImageTrans = new Transform[5];
		towerSelectionImageTrans[0] = transform.Find("TSCanvas/FireTower");
        towerSelectionImageTrans[1] = transform.Find("TSCanvas/EarthTower");
        towerSelectionImageTrans[2] = transform.Find("TSCanvas/WaterTower");
        towerSelectionImageTrans[3] = transform.Find("TSCanvas/MetalTower");
        towerSelectionImageTrans[4] = transform.Find("TSCanvas/WoodTower");

		Vector2 dir1 = new Vector3 (0, 1);
		Vector2 dir2 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18)); // (cos18, sin18, z)
		Vector2 dir3 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54)); // (cos54,  sin54, z)
		Vector2 dir4 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54)); // (-cos36, -sin36, z)
		Vector2 dir5 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18)); // (-cos18, sin18, z)

		Vector2 end1 = dir1 * radius;
		Vector2 end2 = dir2 * radius;
		Vector2 end3 = dir3 * radius;
		Vector2 end4 = dir4 * radius;
		Vector2 end5 = dir5 * radius;

		float frag1 = 0f, frag2 = 0f, frag3 = 0f, frag4 = 0f, frag5 = 0f;

		while(distance < radius){

			frag1 += speed * Time.deltaTime / Vector2.Distance (Vector2.zero, end1);
			frag2 += speed * Time.deltaTime / Vector2.Distance (Vector2.zero, end2);
			frag3 += speed * Time.deltaTime / Vector2.Distance (Vector2.zero, end3);
			frag4 += speed * Time.deltaTime / Vector2.Distance (Vector2.zero, end4);
			frag5 += speed * Time.deltaTime / Vector2.Distance (Vector2.zero, end5);

			towerSelectionImageTrans [0].GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (
					Vector2.zero, dir1 * radius, frag1);
			towerSelectionImageTrans [1].GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (
					Vector2.zero, dir2 * radius, frag2);
			towerSelectionImageTrans [2].GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (
					Vector2.zero, dir3 * radius, frag3);
			towerSelectionImageTrans [3].GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (
					Vector2.zero, dir4 * radius, frag4);
			towerSelectionImageTrans [4].GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp (
					Vector2.zero, dir5 * radius, frag5);

			distance += speed * Time.deltaTime;
		}
		
//		while (distance < radius) {
//			Debug.Log ("tower icon is moving out!");
//			Vector3 dir1 = new Vector3 (0, 1, 0);
//			Vector3 dir2 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18), 0); // (cos18, sin18, z)
//			Vector3 dir3 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54), 0); // (cos54,  sin54, z)
//			Vector3 dir4 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54), 0); // (-cos36, -sin36, z)
//			Vector3 dir5 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18), 0); // (-cos18, sin18, z)
//			towerSelectionImageTrans [0].GetComponent<RectTransform> ().transform.Translate (dir1 * speed * Time.deltaTime /* * newScale */);
//			towerSelectionImageTrans [1].GetComponent<RectTransform> ().transform.Translate (dir2/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
//			towerSelectionImageTrans [2].GetComponent<RectTransform> ().transform.Translate (dir3/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
//			towerSelectionImageTrans [3].GetComponent<RectTransform> ().transform.Translate (dir4/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
//			towerSelectionImageTrans [4].GetComponent<RectTransform> ().transform.Translate (dir5/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
//			distance += speed * Time.deltaTime;
//		}
	}

//	void Update()
//	{
//		myHit = gameTouch.hit;
//		//scaleTowerSelectionPanel2Camera ();
//	}
//
//	void scaleTowerSelectionPanel2Camera()
//	{
//		float cameraSize;
//		float newScale;
//
//		// change the scale of tower selection panel, in relation to the FOV or size of camera
//		if (myCamera.orthographic) {
//			// If the camera is orthographic...
//			cameraSize = myCamera.orthographicSize;
//			// if linear regression
//			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
//			// if Logarithmic regression
//			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
//            for(int i = 0; i < 5; i++)
//            {
//                towerSelectionImageTrans[i].GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
//            }
//        } else {
//			// else the camera is perspective...
//			cameraSize = myCamera.fieldOfView;
//			// if linear regression
//			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
//			// if Logarithmic regression
//			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
//            for(int i = 0; i < 5; i++)
//            {
//                towerSelectionImageTrans[i].GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
//            }
//        }
//	}
}
