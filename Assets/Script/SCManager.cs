﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SCManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }


    void Update()
    {
        text.text = "SCORE: " + score;
    }
}