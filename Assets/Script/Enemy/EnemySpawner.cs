﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	float spawnCD = 0.5f;
	float spawnCDremaining = 2.5f;
    public int creepSum;

	[System.Serializable]
	public class WaveComponent {
		public GameObject enemyPrefab;
		public int num;
		[System.NonSerialized]
		public int spawned = 0;
	}

	public WaveComponent[] waveComps;

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
		spawnCDremaining -= Time.deltaTime;
		if(spawnCDremaining < 0) {
			spawnCDremaining = spawnCD;

			bool didSpawn = false;

			// Go through the wave comps until we find something to spawn;
			foreach(WaveComponent wc in waveComps) {
				if(wc.spawned < wc.num) {
					// Spawn it!
					wc.spawned++;
					Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);

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
