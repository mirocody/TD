using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public static int fixedtime;
    public Text text;
    bool isOver;
    public int curWave;
    public int timer;
    void Awake()
    {
        text = GetComponent<Text>();
        fixedtime=10;
        timer = fixedtime;
        isOver = false;
        curWave = WaveManager.wave;
        InvokeRepeating("CountDown",0,1);
    }

/*
	void Update () {
        if (WaveManager.wave != curWave)
        {
            fixedtime = 60;
            isOver = false;
            curWave = WaveManager.wave;
            text.text = "TIME: 1:00";
        }
	}
*/


    void CountDown()
    {
        if (!isOver)
        {
            timer--;
            int min = timer / 60;
            int second = (timer - min * 60);
            text.text = "TIME: " + min.ToString() + ":" + second.ToString().PadLeft(2,'0');
            if (timer == 0)
            {
                isOver = true;
            }
        }
        else{

          timer=fixedtime;
          WaveManager.wave++;
          curWave++;
          isOver=false;
        }
    }
}
