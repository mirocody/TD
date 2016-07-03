using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static int wave;


    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        wave = 0;
      //  InvokeRepeating("CountDown", TimeManager., 30);
    }


    void Update()
    {
        text.text = wave.ToString();
    }

}
