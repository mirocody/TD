using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {
    
	bool isPause;

	void Awake()
    {
        isPause = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickPauseButton()
    {
        Debug.Log("here");
        if (Time.timeScale!=0)
        {
            Time.timeScale = 0;
            transform.parent.Find("PausePanel").gameObject.SetActive(true);
            isPause = true;
            Debug.Log("Pause");
        }
    }
}
