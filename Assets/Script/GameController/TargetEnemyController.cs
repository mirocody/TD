using UnityEngine;
using System.Collections;

public class TargetEnemyController : MonoBehaviour {
    public static Transform targetEnemey;
    GameTouchHandler gameTouch;
    RaycastHit myHit;
    // Use this for initialization
    void Start () {
        gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameTouch.isEnemyTapped)
        {
            myHit = gameTouch.hit;
            if (targetEnemey == null || targetEnemey != myHit.transform)
            {
                if (targetEnemey != null) targetEnemey.GetComponent<Enemy>().my_Arrow.SetActive(false);
                targetEnemey = myHit.transform;
                targetEnemey.GetComponent<Enemy>().my_Arrow.SetActive(true);
            }
            else
            {
                targetEnemey.GetComponent<Enemy>().my_Arrow.SetActive(false);
                targetEnemey = null;
            }
        }
	}
}
