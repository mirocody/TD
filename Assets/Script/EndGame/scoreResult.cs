using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class scoreResult : MonoBehaviour {

	Text txt;
	private int currentscore=0;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text>();
		txt.text="Score : " + currentscore;
	}

	// Update is called once per frame
	void Update () {
		txt.text="Score : " + currentscore;  
		currentscore = EndScore.score;
		 
	}
}
