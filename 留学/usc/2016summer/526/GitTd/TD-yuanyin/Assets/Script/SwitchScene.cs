using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	public void changeScene(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
