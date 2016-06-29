using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldManager : MonoBehaviour
{
    public static int gold;
    Text text;
	InitialData initData;


    void Start()
    {
		initData = GameObject.Find("SystemData").GetComponent<InitialData>();
		text = GetComponent<Text>();
		gold = initData.gold;
    }


    void Update()
    {
      text.text = gold.ToString();
    }
}
