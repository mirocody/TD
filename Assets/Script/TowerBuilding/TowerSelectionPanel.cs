using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour {

	[HideInInspector]
	public bool isTowerSelected;

	[HideInInspector]
	public GameObject selectedTower;

	private Camera myCamera;
	private Transform[] towerSelectionImageTrans;
	private TowerSpotTouch towerSpotTouch;
    public bool init = true;
    private float speed = 250f;
    public float distance = 0;
	void Start()
	{
        towerSelectionImageTrans = new Transform[5];
		towerSelectionImageTrans[0] = transform.Find("TSCanvas/FireTower");
        towerSelectionImageTrans[1] = transform.Find("TSCanvas/EarthTower");
        towerSelectionImageTrans[2] = transform.Find("TSCanvas/WaterTower");
        towerSelectionImageTrans[3] = transform.Find("TSCanvas/MetalTower");
        towerSelectionImageTrans[4] = transform.Find("TSCanvas/WoodTower");
        towerSpotTouch = GameObject.Find("TowerBuildController").GetComponent<TowerSpotTouch> ();
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();
	}

	void Update()
	{
		scaleTowerSelectionPanel2Camera ();
		anchorTowerSelectionPanel2TowerSpot ();
	}

	void scaleTowerSelectionPanel2Camera()
	{
		float cameraSize;
		float newScale;

		// change the scale of tower selection panel, in relation to the FOV or size of camera, and also screen
		if (myCamera.orthographic) {
			// If the camera is orthographic...
			cameraSize = myCamera.orthographicSize;
			// if linear regression
			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
			// if Logarithmic regression
			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
            for(int i = 0; i < 5; i++)
            {
                towerSelectionImageTrans[i].GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
            }
        } else {
			// else the camera is perspective...
			cameraSize = myCamera.fieldOfView;
			// if linear regression
			//newScale = Mathf.Clamp (-0.24f * cameraSize + 5.65f, 1.0f, 5.0f);
			// if Logarithmic regression
			newScale = Mathf.Clamp (-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
            for(int i = 0; i < 5; i++)
            {
                towerSelectionImageTrans[i].GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1.0f);
            }
        }
	}

	void anchorTowerSelectionPanel2TowerSpot()
	{

        // move the tower selection panel to the touch point
        if (init)
        {
            for (int i = 0; i < 5; i++)
            {
                towerSelectionImageTrans[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                towerSpotTouch.touchPos.x,
                towerSpotTouch.touchPos.y
            );
            }
            init = false;
        }
        float cameraSize = myCamera.orthographicSize;
        float newScale = Mathf.Clamp(-2.657f * Mathf.Log(cameraSize) + 9.0f, 1.0f, 5.0f);
        if (distance < 50* newScale)
        {
            Vector3 dir1 = new Vector3(0, 1, 0);
            Vector3 dir2 = new Vector3(1,0.3249f,0);
            Vector3 dir3 = new Vector3(1, -1.3764f, 0);
            Vector3 dir4 = new Vector3(-1, -1.3764f, 0);
            Vector3 dir5 = new Vector3(-1, 0.3249f, 0);
            distance +=(speed*Time.deltaTime);
            towerSelectionImageTrans[0].GetComponent<RectTransform>().transform.Translate(dir1 * speed * Time.deltaTime * newScale);
            towerSelectionImageTrans[1].GetComponent<RectTransform>().transform.Translate(dir2.normalized * speed * Time.deltaTime * newScale);
            towerSelectionImageTrans[2].GetComponent<RectTransform>().transform.Translate(dir3.normalized * speed * Time.deltaTime * newScale);
            towerSelectionImageTrans[3].GetComponent<RectTransform>().transform.Translate(dir4.normalized * speed * Time.deltaTime * newScale);
            towerSelectionImageTrans[4].GetComponent<RectTransform>().transform.Translate(dir5.normalized * speed * Time.deltaTime * newScale);
        }
    }

	public void SelectTowerType(GameObject prefab) {
		selectedTower = prefab;
		isTowerSelected = true;
		//Debug.Log ("Tower Selected!");
	}

	public void CancelSelectTower()
	{
		gameObject.SetActive (false);
	}
}
