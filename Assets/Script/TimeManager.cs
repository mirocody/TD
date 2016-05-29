using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public static int fixedtime;
    public Text text;
    bool isOver;
    public int curWave;
	
    void Awake()
    {
        text = GetComponent<Text>();
        fixedtime = 60;
        isOver = false;
        curWave = WaveManager.wave;
        InvokeRepeating("CountDown",0,1);
    }

	
	void Update () {
        if (WaveManager.wave != curWave)
        {
            fixedtime = 60;
            isOver = false;
            curWave = WaveManager.wave;
            text.text = "TIME: 1:00";
        }
	}

    void CountDown()
    {
        if (!isOver)
        {
            fixedtime--;
            int min = fixedtime / 60;
            int second = (fixedtime - min * 60);
            text.text = "TIME: " + min.ToString() + ":" + second.ToString().PadLeft(2,'0');
            if (fixedtime == 0)
            {
                isOver = true;
            }
        }
    }
}
