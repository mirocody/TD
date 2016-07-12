using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class EndScore : MonoBehaviour {

	public bool spawnerOver; // will be modify by the SpawerManager.cs
	public static int score;
	public static int gold;
	public static int hp;
	private float remainingTime;
	private bool gameOverSceneShow;
	//public bool gameOver = false;

	void  Start (){
		spawnerOver = false;
		score = 0;
		gold = 0;
		hp = 0;
		remainingTime = 2.0f;
		gameOverSceneShow = false;
	}

	void  Update (){
		//int count = 10;
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
		score=ScoreManager.score;
		gold=GoldManager.gold;
		hp=HPManager.hp;
		Debug.Log (SpawnerManager.stillAlive);
		if (spawnerOver && SpawnerManager.stillAlive<=0 ) {
			//Application.LoadLevel("Menu");
			Debug.Log(remainingTime);
			remainingTime-=(1*Time.deltaTime);
			if (remainingTime <= 0)
				gameOverSceneShow = true;

					
		}

		if (gameOverSceneShow) {
			GameOverScene ();
		}
		//else 
			//gameOver = false;


	}

	void GameOverScene(){
		Time.timeScale=1;

		//Debug.Log("Name:"+SceneManager.GetActiveScene ().name);//level1
		string cur = SceneManager.GetActiveScene ().name;
		string next = cur.Substring (0, cur.Length - 1);//level
		string nextNum = cur.Substring (cur.Length - 1, 1);//1
		//int nexNum = (int)nextNum + 1;
		//Debug.Log("cur:"+cur);//level1
		//Debug.Log("Next:"+next);//level1
		if (nextNum == "1") {
			SceneManager.LoadScene ("Scoreforlevel1");
		}

		/*gameOver = true;
			if (gameOver)*/
		Debug.Log ("GAME OVER!!");
	}
}
