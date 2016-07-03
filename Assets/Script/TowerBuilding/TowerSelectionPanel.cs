using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour
{
    public float radius;

    //private Camera myCamera;
    Transform[] towerSelectionImageTrans;
	Transform[] towerCostTxtTrans;
    GameTouchHandler gameTouch;
	TowerData towerData;
	TowerBuildController tbController;
    float speed = 300f;
    float distance = 0;
    RaycastHit myHit;
    Color[] originColor;

    void Start()
    {
        gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler>();
		tbController = GameObject.Find ("TowerBuild").GetComponent<TowerBuildController> ();
        myHit = gameTouch.hit;
        // Get all selection buttons.
        towerSelectionImageTrans = new Transform[5];
        originColor = new Color[5];
        towerSelectionImageTrans[0] = transform.Find("TSCanvas/EarthTower");
        towerSelectionImageTrans[1] = transform.Find("TSCanvas/WoodTower");
        towerSelectionImageTrans[2] = transform.Find("TSCanvas/MetalTower");
        towerSelectionImageTrans[3] = transform.Find("TSCanvas/FireTower");
        towerSelectionImageTrans[4] = transform.Find("TSCanvas/WaterTower");
        for(int i = 0; i < towerSelectionImageTrans.Length; i++)
        {
            originColor[i] = towerSelectionImageTrans[i].GetComponent<Image>().color;
        }
        setColor();
		// Get all cost txt
		towerCostTxtTrans = new Transform[5];
		towerCostTxtTrans [0] = transform.Find ("TSCanvas/EarthTower/EarthTowerCostTxt");
		towerCostTxtTrans [1] = transform.Find ("TSCanvas/WoodTower/WoodTowerCostTxt");
		towerCostTxtTrans [2] = transform.Find ("TSCanvas/MetalTower/MetalTowerCostTxt");
		towerCostTxtTrans [3] = transform.Find ("TSCanvas/FireTower/FireTowerCostTxt");
		towerCostTxtTrans [4] = transform.Find ("TSCanvas/WaterTower/WaterTowerCostTxt");

        //Set initial position for these buttons.
        for(int i = 0; i < 5; i++)
        {
            towerSelectionImageTrans[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                Camera.main.WorldToScreenPoint(myHit.transform.position).x,
                Camera.main.WorldToScreenPoint(myHit.transform.position).y
            );
        }

		for (int i = 0; i < 5; i++) {
			towerCostTxtTrans [i].GetComponent<Text> ().text = tbController.costs [i].ToString();
		}
    }

    void Update()
    {
        setColor();
        UpdateTowerSelectionPanelPos();

    }

    void UpdateTowerSelectionPanelPos()
    {
        if (distance < radius)
        {
            Vector3 dir1 = new Vector3(0, 1, 0);
            Vector3 dir2 = new Vector3(1, 0.3249f, 0);
            Vector3 dir3 = new Vector3(1, -1.3764f, 0);
            Vector3 dir4 = new Vector3(-1, -1.3764f, 0);
            Vector3 dir5 = new Vector3(-1, 0.3249f, 0);
            distance += (speed * 0.02474084f);
            towerSelectionImageTrans[0].GetComponent<RectTransform>().transform.Translate(dir1 * speed * 0.02474084f);
            towerSelectionImageTrans[1].GetComponent<RectTransform>().transform.Translate(dir2.normalized * speed * 0.02474084f);
            towerSelectionImageTrans[2].GetComponent<RectTransform>().transform.Translate(dir3.normalized * speed * 0.02474084f);
            towerSelectionImageTrans[3].GetComponent<RectTransform>().transform.Translate(dir4.normalized * speed * 0.02474084f);
            towerSelectionImageTrans[4].GetComponent<RectTransform>().transform.Translate(dir5.normalized * speed * 0.02474084f);
        }
    }

    void setColor()
    {
        for(int i = 0; i < 5; i++)
        {
            if (GoldManager.gold < tbController.costs[i])
            {
                towerSelectionImageTrans[i].GetComponent<Image>().color = new Color(originColor[i].r, originColor[i].g, originColor[i].b,0.5f);
            }
            else
            {
                towerSelectionImageTrans[i].GetComponent<Image>().color = originColor[i];
            }
        }
    }
}