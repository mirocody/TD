using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausePanelManager : MonoBehaviour {

    bool isPlay;

	// Use this for initialization
	void Start () {
        AudioListener.pause = false;
        isPlay = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClosePanel()
    {
        Time.timeScale = 1;
        transform.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ShutDownBGM(bool isChecked)
    {
        if (isPlay)
        {
            AudioListener.pause = true;
            isPlay = false;
        }
        else
        {
            AudioListener.pause = false;
            isPlay = true;
        }
    }
}
