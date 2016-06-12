using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {
    bool isPause;
	// Use this for initialization
	void Awake()
    {
        isPause = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickPauseButton()
    {
        if (Time.timeScale!=0)
        {
            Time.timeScale = 0;
            transform.parent.Find("PausePanel").gameObject.SetActive(true);
            isPause = true;
            Debug.Log("Pause");
        }
    }
}
