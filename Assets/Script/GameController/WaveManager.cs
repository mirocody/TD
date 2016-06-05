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
        wave = 10;
        InvokeRepeating("CountDown", 30, 30);
    }


    void Update()
    {
        text.text = "WAVE: " + wave;
    }

    void CountDown()
    {
        wave--;
    }
}
