using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

    public void ClickPauseButton()
    {
        if (Time.timeScale!=0)
        {
            Time.timeScale = 0;
            transform.parent.Find("PausePanel").gameObject.SetActive(true);
            Debug.Log("Pause");
        }
    }
}
