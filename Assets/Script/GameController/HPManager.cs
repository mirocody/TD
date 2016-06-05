using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPManager : MonoBehaviour
{
    public static int hp;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        hp = 50;
    }


    void Update()
    {
        text.text = "HP: " + hp;
    }
}
