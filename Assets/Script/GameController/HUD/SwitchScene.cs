using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	public void changeScene(string sceneName) {
		//Application.LoadLevel(sceneName);
		SceneManager.LoadScene (sceneName);

	}
}
