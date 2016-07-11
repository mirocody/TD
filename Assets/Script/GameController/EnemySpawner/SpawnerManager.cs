using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {
    
	bool isStart;
	public static int curSpawner;
	public static int stillAlive;
	private int count;
	//private int wave;
	//private int enemynoleft;
	EndScore endScore;

	void Start () {
        stillAlive = 0;
        curSpawner = 0;
        isStart = true;
        count = transform.childCount;
		Debug.Log ("Count:" + count);
		endScore = GameObject.Find ("EndScoreManager").GetComponent<EndScore>();
		endScore.spawnerOver = false;
	}
	
	void Update () {
		

		if (WaveManager.wave < count)
		{
			Debug.Log ("Wave in:" + WaveManager.wave);
			Debug.Log ("Count in:" + count);
			if (isStart || TimeManager.timer == 0 || stillAlive==0)
			{
				GameObject temp=transform.GetChild(curSpawner).gameObject;
				temp.SetActive(true);
				curSpawner++;
				WaveManager.wave++;
				if (WaveManager.wave > 5) {
					GameObject.Find ("BGM").transform.GetComponent<AudioController> ().changeToBGM2 ();
				}
				isStart = false;
			}


		}
		//if (WaveManager.wave == count && stillAlive==0) {
		if (WaveManager.wave == count) {
			Debug.Log ("Wave:" + WaveManager.wave);
			Debug.Log ("Count:" + count);
			endScore.spawnerOver = true;
		}
	}
}
