using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {
    
	bool isStart;
    public static int curSpawner;
    public static int stillAlive;

	void Start () {
        stillAlive = 0;
        curSpawner = 0;
        isStart = true;
	}
	
	void Update () {
        if (WaveManager.wave < transform.childCount)
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
