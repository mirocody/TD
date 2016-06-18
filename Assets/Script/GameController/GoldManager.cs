using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldManager : MonoBehaviour
{
    public static int gold;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        gold = 100;
    }


    void Update()
    {
      text.text = gold.ToString();
    }
}
