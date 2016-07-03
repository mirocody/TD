using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class InitialData : MonoBehaviour
{
	// stat for enemy (6 types, 6 sets in total)
	public float[] enemyhealth = new float[6];
	public int[] enemygold = new int[6];
	public int[] enemyscore = new int[6];
	public float[] enemyspeed = new float[6];

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

	// load csv
	public TextAsset table_hud;
	public TextAsset table_enemy;
	public TextAsset table_tower_bullet;
    public TextAsset table_random;

    //present properties.
    public static int randomTower;
    public static int fireCardNum;
    public static int woodCardNum;
    public static int metalCardNum;
    public static int waterCardNum;
    public static int earthCardNum;
    System.Random r = new System.Random();

    //Present drop Probability
    public static int spotProbability;
    public static int elementProbability;
    public static int towerProbability;


    void Awake() {
        //Set values for present properties
        randomTower = r.Next(80, 85);
        fireCardNum = 0;
        woodCardNum = 0;
        metalCardNum = 0;
        waterCardNum = 0;
        earthCardNum = 0;

        // read hud data from table_hud
        string[] hud = table_hud.text.Split('\n');
		for(int i = 0; i < 2; i++) {
			string[] splits = hud[i].Split (',');
			if(i==0) {
				
				gold = int.Parse(splits[1]);
			}
			if(i==1) {
				hp = int.Parse(splits[1]);
			}
		
		}

		// read enemy data from table_enemy
		string[] enemy = table_enemy.text.Split('\n');
		for (int i = 0; i < enemy.Length - 1; i++) {
			//Debug.Log("test table_enemy");
			//Debug.Log (i);
			//Debug.Log (enemyhealth.Length);
			String[] splits = enemy [i+1].Split(',');
			enemyhealth[i] = int.Parse(splits[0]);
			enemygold[i] = int.Parse(splits[1]);
			enemyscore[i] = int.Parse(splits[2]);
			enemyspeed[i] = float.Parse(splits[3]);
		}


		// read tower and bullet data from table_tower_bullet
		string[] tower_bullet = table_tower_bullet.text.Split ('\n');
		for (int j = 0; j < 3; j++) {
			for (int i = 0; i < 5; i++) {
				//Debug.Log("test table_tower");
				String[] splits = tower_bullet[i+1+6*j].Split(',');

				range[i,j] = int.Parse(splits[0]);
				cost[i,j] = int.Parse(splits[1]);
				upgradeCost[i,j] = int.Parse(splits[2]);
				turnSpeed[i,j] = float.Parse(splits[3]);
				errorAmount[i,j] = int.Parse(splits[4]);
				fireCooldown[i,j] = float.Parse(splits[5]);
				fireCooldownLeft[i,j] = int.Parse(splits[6]);
				rechargeRate[i,j] = float.Parse(splits[7]);
				elevateRate[i,j] = float.Parse(splits[8]);
				elevateRadius[i,j] = float.Parse(splits[9]);
				speed[i,j] = float.Parse(splits[10]);
				damage[i,j] = float.Parse(splits[11]);
				radius[i,j] = float.Parse(splits[12]);
			}
		}

        setProbabilityValue();
    }
    void Update()
    {
        randomTower = r.Next(80, 85);
    }

    void setProbabilityValue()
    {
        String[] randomValue = table_random.text.Split('\n');
        spotProbability = int.Parse(randomValue[0].Split(',')[1]);
        elementProbability = int.Parse(randomValue[1].Split(',')[1]);
        towerProbability = int.Parse(randomValue[2].Split(',')[1]);
    }
}
