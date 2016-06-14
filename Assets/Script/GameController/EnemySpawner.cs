using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int creepSum;
	[System.Serializable]
	public class WaveComponent {
		public GameObject enemyPrefab;
		public int num;
		public float spawnInterval;
		[System.NonSerialized]
		public int spawned = 1;
	}

	public WaveComponent[] waveComps;

	//float spawnCD = 0.75f;
	float nextSpawnRemaining = 1.0f;
	float spawnSpotOffset = 2.0f;
	//float spawnCDremaining = 10f;

	// Use this for initialization
	void Start () {
        creepSum = 0;
        foreach (WaveComponent wc in waveComps)
        {
            creepSum += wc.num;
        }
        SpawnerManager.stillAlive += creepSum;

    }
	
	// Update is called once per frame
	void Update () {
		nextSpawnRemaining -= Time.deltaTime;
		if(nextSpawnRemaining < 0) {
			//spawnCDremaining = spawnCD;

			bool didSpawn = false;

			// Go through the wave comps until we find something to spawn;
			foreach(WaveComponent wc in waveComps) {
				nextSpawnRemaining = wc.spawnInterval;
				if(wc.spawned < wc.num) {
					// Spawn it!
					wc.spawned++;
					//Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);
					Vector3 offset = new Vector3(Random.Range(-spawnSpotOffset, spawnSpotOffset), 0, Random.Range(-spawnSpotOffset, spawnSpotOffset));
					Instantiate(wc.enemyPrefab, this.transform.position + offset, this.transform.rotation);

					didSpawn = true;
					break;
				}
			}

			if(didSpawn == false) {
                // Wave must be complete!
                // TODO: Instantiate next wave object!

                SpawnerManager.curSpawner--;

				Destroy(gameObject);
			}
		}
	}
}
