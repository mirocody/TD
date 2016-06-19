using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour {

	void Start () {
	
	}
	
	
	void Update () {
	
	}
    public static void clickPauseButton()
    {
        GameObject.Find("HUD").transform.Find("PausePanel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public static void clickCloseButton()
    {
        GameObject.Find("HUD").transform.Find("PausePanel").gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public static void clickRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void clickQuitButton()
    {
        Application.Quit();
    }
}
