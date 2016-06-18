using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPManager : MonoBehaviour
{
    public static int hp;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        hp = 50;
    }


    void Update()
    {
        text.text = hp.ToString();
    }

	public void LoseLife(int l = 1) {
		hp -= l;
		if(hp <= 0) {
			GameOver();
		}
	}

	public void GameOver() {
		Debug.Log("Game Over");
		// TODO: Send the player to a game-over screen instead!
		Transform temp = transform.parent.Find("GameOverPanel");
		temp.gameObject.SetActive(true);
		temp.Find("ScoreText").GetComponent<Text>().text= "SCORE: " + ScoreManager.score;
		Time.timeScale = 0;
	}
}
