using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour
{
    public float radius;

    //private Camera myCamera;
    private Transform[] towerSelectionImageTrans;
    GameTouchHandler gameTouch;
    //public bool init = true;
    private float speed = 300f;
    private float distance = 0;
    RaycastHit myHit;

    void Start()
    {
        gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler>();
        myHit = gameTouch.hit;
        //Get all selection buttons.
        towerSelectionImageTrans = new Transform[5];
        towerSelectionImageTrans[0] = transform.Find("TSCanvas/FireTower");
        towerSelectionImageTrans[1] = transform.Find("TSCanvas/EarthTower");
        towerSelectionImageTrans[2] = transform.Find("TSCanvas/WaterTower");
        towerSelectionImageTrans[3] = transform.Find("TSCanvas/MetalTower");
        towerSelectionImageTrans[4] = transform.Find("TSCanvas/WoodTower");
        //Set initial position for these buttons.
        for(int i = 0; i < 5; i++)
        {
            towerSelectionImageTrans[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                Camera.main.WorldToScreenPoint(myHit.transform.position).x,
                Camera.main.WorldToScreenPoint(myHit.transform.position).y
            );
        }
    }

    void Update()
    {
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
            distance += (speed * Time.deltaTime);
            towerSelectionImageTrans[0].GetComponent<RectTransform>().transform.Translate(dir1 * speed * Time.deltaTime);
            towerSelectionImageTrans[1].GetComponent<RectTransform>().transform.Translate(dir2.normalized * speed * Time.deltaTime);
            towerSelectionImageTrans[2].GetComponent<RectTransform>().transform.Translate(dir3.normalized * speed * Time.deltaTime);
            towerSelectionImageTrans[3].GetComponent<RectTransform>().transform.Translate(dir4.normalized * speed * Time.deltaTime );
            towerSelectionImageTrans[4].GetComponent<RectTransform>().transform.Translate(dir5.normalized * speed * Time.deltaTime);
        }
    }
}