using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class InitialData : MonoBehaviour
{
	// stat for enemy (5 types, 5 sets in total)
	public float[] enemyhealth = new float[5];
	public int[] enemygold = new int[5];
	public int[] enemyscore = new int[5];
	public float[] enemyspeed = new float[5];

	// stat for tower and bullet (5 types of tower, 3 levels, 15 sets in total)
	// nameAB stands for: variable name, at type A, level B
	public float[,] range = new float[5,3];
	public int[,] cost = new int[5,3];
	public int[,] upgradeCost = new int[5,3];
	public float[,] turnSpeed = new float[5,3];
	public float[,] errorAmount = new float[5,3];
	public float[,] fireCooldown = new float[5,3];
	public int[,] fireCooldownLeft = new int[5,3];
	public float[,] rechargeRate = new float[5,3];

	//new added
	public float[,] elevateRate = new float[5,3];
	public float[,] elevateRadius = new float[5,3];

	public float[,] speed = new float[5,3];
	public float[,] damage = new float[5,3];
	public float[,] radius = new float[5,3];

	// stat for HUD
	public int gold = 0;
	public int hp = 0;

	void Awake() {

		// read hud data from table_hud
		using (var rd = new StreamReader("table_hud.csv"))
		{
			int i = 0;
			while (!rd.EndOfStream)
			{
				var splits = rd.ReadLine().Split(',');
				if(i==0) {
					gold = Convert.ToInt32(splits[1]);
				}
				if(i==1) {
					hp = Convert.ToInt32(splits[1]);
				}
				i++;
			}
		}

		// read enemy data from table_enemy
		using (var rd = new StreamReader("table_enemy.csv"))
		{
			int i = 0;
			rd.ReadLine();
			while (!rd.EndOfStream)
			{
				var splits = rd.ReadLine().Split(',');
				enemyhealth[i] = Convert.ToSingle(splits[0]);
				enemygold[i] = Convert.ToInt32(splits[1]);
				enemyscore[i] = Convert.ToInt32(splits[2]);
				enemyspeed[i] = Convert.ToSingle(splits[3]);
				i++;
			}
		}

		// read tower and bullet data from table_tower_bullet
		using (var rd = new StreamReader("table_tower_bullet.csv"))
		{
			int i = 0;
			rd.ReadLine();
			while (!rd.EndOfStream && i<5)
			{
				var splits = rd.ReadLine().Split(',');

				range[i,0] = Convert.ToSingle(splits[0]);
				cost[i,0] = Convert.ToInt32(splits[1]);
				upgradeCost[i,0] = Convert.ToInt32(splits[2]);
				turnSpeed[i,0] = Convert.ToSingle(splits[3]);
				errorAmount[i,0] = Convert.ToSingle(splits[4]);
				fireCooldown[i,0] = Convert.ToSingle(splits[5]);
				fireCooldownLeft[i,0] = Convert.ToInt32(splits[6]);
				rechargeRate[i,0] = Convert.ToSingle(splits[7]);
				elevateRate[i,0] = Convert.ToSingle(splits[8]);
				elevateRadius[i,0] = Convert.ToSingle(splits[9]);
				speed[i,0] = Convert.ToSingle(splits[10]);
				damage[i,0] = Convert.ToSingle(splits[11]);
				radius[i,0] = Convert.ToSingle(splits[12]);
				i++;
			}

			rd.ReadLine();
			while (!rd.EndOfStream && i<10)
			{
				var splits = rd.ReadLine().Split(',');

				range[i-5,1] = Convert.ToSingle(splits[0]);
				cost[i-5,1] = Convert.ToInt32(splits[1]);
				upgradeCost[i-5,1] = Convert.ToInt32(splits[2]);
				turnSpeed[i-5,1] = Convert.ToSingle(splits[3]);
				errorAmount[i-5,1] = Convert.ToSingle(splits[4]);
				fireCooldown[i-5,1] = Convert.ToSingle(splits[5]);
				fireCooldownLeft[i-5,1] = Convert.ToInt32(splits[6]);
				rechargeRate[i-5,1] = Convert.ToSingle(splits[7]);
				elevateRate[i-5,1] = Convert.ToSingle(splits[8]);
				elevateRadius[i-5,1] = Convert.ToSingle(splits[9]);
				speed[i-5,1] = Convert.ToSingle(splits[10]);
				damage[i-5,1] = Convert.ToSingle(splits[11]);
				radius[i-5,1] = Convert.ToSingle(splits[12]);
				i++;
			}

			rd.ReadLine();
			while (!rd.EndOfStream)
			{
				var splits = rd.ReadLine().Split(',');

				range[i-10,2] = Convert.ToSingle(splits[0]);
				cost[i-10,2] = Convert.ToInt32(splits[1]);
				upgradeCost[i-10,2] = Convert.ToInt32(splits[2]);
				turnSpeed[i-10,2] = Convert.ToSingle(splits[3]);
				errorAmount[i-10,2] = Convert.ToSingle(splits[4]);
				fireCooldown[i-10,2] = Convert.ToSingle(splits[5]);
				fireCooldownLeft[i-10,2] = Convert.ToInt32(splits[6]);
				rechargeRate[i-10,2] = Convert.ToSingle(splits[7]);
				elevateRate[i-10,2] = Convert.ToSingle(splits[8]);
				elevateRadius[i-10,2] = Convert.ToSingle(splits[9]);
				speed[i-10,2] = Convert.ToSingle(splits[10]);
				damage[i-10,2] = Convert.ToSingle(splits[11]);
				radius[i-10,2] = Convert.ToSingle(splits[12]);
				i++;
			}
		}

	}
}
