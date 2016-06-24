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
            targetEnemey = myHit.transform;
        }
	}
}
