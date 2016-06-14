using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour {

	public float yOffset;
	public float radius;

	//private Camera myCamera;
	private Transform[] towerSelectionImageTrans;
	//private Transform TSCanvas;
	//private TowerSpotTouch towerSpotTouch;
	//GameTouchHandler gameTouch;
    //public bool init = true;
    private float speed = 0.5f;
    private float distance = 0;
	RaycastHit myHit;

	void Start()
	{
        towerSelectionImageTrans = new Transform[5];
		towerSelectionImageTrans[0] = transform.Find("TSCanvas/FireTower");
        towerSelectionImageTrans[1] = transform.Find("TSCanvas/EarthTower");
        towerSelectionImageTrans[2] = transform.Find("TSCanvas/WaterTower");
        towerSelectionImageTrans[3] = transform.Find("TSCanvas/MetalTower");
        towerSelectionImageTrans[4] = transform.Find("TSCanvas/WoodTower");
		//TSCanvas = transform.Find ("TSCanvas");
        //towerSpotTouch = GameObject.Find("TowerBuildController").GetComponent<TowerSpotTouch> ();
		//gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler> ();
		//myCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();

		while (distance < radius) {
			Vector3 dir1 = new Vector3 (0, 1, 0);
			Vector3 dir2 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18), 0); // (cos18, sin18, z)
			Vector3 dir3 = new Vector3 (Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54), 0); // (cos54,  sin54, z)
			Vector3 dir4 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 54), -Mathf.Sin (Mathf.PI / 180f * 54), 0); // (-cos36, -sin36, z)
			Vector3 dir5 = new Vector3 (-Mathf.Cos (Mathf.PI / 180f * 18), Mathf.Sin (Mathf.PI / 180f * 18), 0); // (-cos18, sin18, z)
			towerSelectionImageTrans [0].GetComponent<RectTransform> ().transform.Translate (dir1 * speed * Time.deltaTime /* * newScale */);
			towerSelectionImageTrans [1].GetComponent<RectTransform> ().transform.Translate (dir2/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
			towerSelectionImageTrans [2].GetComponent<RectTransform> ().transform.Translate (dir3/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
			towerSelectionImageTrans [3].GetComponent<RectTransform> ().transform.Translate (dir4/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
			towerSelectionImageTrans [4].GetComponent<RectTransform> ().transform.Translate (dir5/*.normalized*/ * speed * Time.deltaTime /* * newScale */);
			distance += speed * Time.deltaTime;
		}
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
