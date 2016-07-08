using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarManager : MonoBehaviour {

	//reference to star images
	private GameObject star1;
	private GameObject star2;
	private GameObject star3;
	private GameObject starE;
	//private GameObject text;
	//reference to next button


	//protected string currentLevel;
	//protected int worldIndex;
	//protected int levelIndex;
	//bool isLevelComplete ;
	//timer text reference
	//public Text timerText;
	//time passed since start of level
	protected float totalTime = 0f;

	//public static int score;
	//public static int lives;
	//Text text;
	//ScoreManager scoreManager;
	// Use this for initialization
	void Start () {
		//set the level complete to false on start of level
		//isLevelComplete = false;
		//get the star images
		star1 = GameObject.Find("starOne");
		star2 = GameObject.Find("starTwo");
		star3 = GameObject.Find("starThree");
		starE = GameObject.Find("starEmpty");
		//get the next button
	
		//disable the image component of all the star images
		star1.GetComponent<Image>().enabled = false;
		star2.GetComponent<Image>().enabled = false;
		star3.GetComponent<Image>().enabled = false;
		starE.GetComponent<Image>().enabled = false;



		//text = GameObject.Find("score").GetComponent<Text>;
		//score = 2;
		//scoreManager=GameObject.Find ("EndScoreManager").GetComponent<score>();

	}

	// Update is called once per frame
	void Update () {
	//		text.GetComponent<Text> = score.ToString ();
		//Debug.Log("Score:"+EndScore.score);
		//Debug.Log("HP:"+EndScore.hp);
		//Debug.Log("Gold:"+EndScore.gold);


		//text.text = EndScore.score.ToString;
		//txt = gameObject.GetComponent<Text>(); 
		//txt.text="Score : " + currentscore;

		if (EndScore.score < 10) {
			starE.GetComponent<Image> ().enabled = true;
			//star2.GetComponent<Image> ().enabled = true;

			//star3.GetComponent<Image> ().enabled = true;
		} else if (EndScore.score < 20) {
			star1.GetComponent<Image> ().enabled = true;
		
		} else if (EndScore.score < 30) {
			star3.GetComponent<Image> ().enabled = true;

		} else {
		
		   star3.GetComponent<Image> ().enabled = true;
		}
		
	
	}



}