using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager: MonoBehaviour
{
    public static int score;
    //public static int lives;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
        //lives=0;
    }

//    public void LoseLife(int l = 1) {
//      lives -= l;
//      if(lives <= 0) {
//        GameOver();
//      }
//    }
//
//    public void GameOver() {
//      Debug.Log("Game Over");
//        // TODO: Send the player to a game-over screen instead!
//        Transform temp = transform.parent.Find("GameOverPanel");
//        temp.gameObject.SetActive(true);
//        temp.Find("ScoreText").GetComponent<Text>().text= "SCORE: " + score;
//        Time.timeScale = 0;
//    }

    void Update()
    {
      text.text = score.ToString();
    }
}
