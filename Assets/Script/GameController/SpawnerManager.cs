using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {
    //public GameObject nextSpawner;
    bool isStart;
    public static int curSpawner;
    public static int stillAlive;
	// Use this for initialization
	void Start () {
        stillAlive = 0;
        curSpawner = 0;
        isStart = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (WaveManager.wave < 11)
        {
            if (isStart || TimeManager.timer == 0||stillAlive==0)
            {
                GameObject temp=transform.GetChild(curSpawner).gameObject;
                temp.SetActive(true);
                curSpawner++;
                WaveManager.wave++;
                isStart = false;
            }
        }
	}
}
