using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public static bool isBGMPlaying=true;
    private static bool isPause = false;
    private static GameObject mark;
    private static Transform pauseButton;

	void Start () {
        mark = GameObject.Find("BGMMark").gameObject;
    }
	
	
	void Update () {
	
	}
    public static void clickPauseButton()
    {
        //GameObject.Find("HUD").transform.Find("PausePanel").gameObject.SetActive(true);
        Transform temp = GameObject.Find("HUD").transform.Find("Pause");
        if (!isPause)
        {
            Time.timeScale = 0;
            isPause = true;
            Debug.Log(GameObject.Find("HUD").transform.Find("Pause"));
            temp.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("play");
        }
        else
        {
            Time.timeScale = 1;
            isPause = false;
            Debug.Log(GameObject.Find("HUD").transform.Find("Pause"));
            temp.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("pause");
        }
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
		//Application.LoadLevel("Menu");
		SceneManager.LoadScene ("Menu");
    }

    public static void changeBGMStatus()
    {
        Debug.Log(mark);
        if (isBGMPlaying)
        {
            isBGMPlaying = false;
            AudioListener.pause = true;
            mark.SetActive(false);
        }
        else
        {
            isBGMPlaying = true;
            AudioListener.pause = false;
            mark.SetActive(true);
        }
    }

    public static void showMenu()
    {
        GameObject.Find("HUD").transform.Find("PausePanel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
