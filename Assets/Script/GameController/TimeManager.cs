using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public static int fixedTime;
    public Text text;
    bool isOver;
    public int curWave;

    void Awake()
    {
        text = GetComponent<Text>();
        fixedTime = 60;
        isOver = false;
        curWave = WaveManager.wave;
        InvokeRepeating("CountDown",0,1);
    }


	void Update () {
        if (WaveManager.wave != curWave)
        {
            fixedTime = 60;
            isOver = false;
            curWave = WaveManager.wave;
            text.text = "TIME: 1:00";
        }
	}

    void CountDown()
    {
        if (!isOver)
        {
            fixedTime--;
            int min = fixedTime / 60;
            int second = (fixedTime - min * 60);
            text.text = "TIME: " + min.ToString() + ":" + second.ToString().PadLeft(2,'0');
            if (fixedTime == 0)
            {
                isOver = true;
            }
        }
    }
}
