using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElementCardController : MonoBehaviour {
    GameTouchHandler gameTouch;
    RaycastHit myHit;
    // Use this for initialization
    void Start () {
        gameTouch = GameObject.Find("GameTouch").GetComponent<GameTouchHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameTouch.isElementCardTapped)
        {
            myHit = gameTouch.hit;
            collectElementCard();
        }
	}

    void collectElementCard()
    {
        ElementCard elementCard = myHit.collider.transform.GetComponent<ElementCard>();
        if (elementCard.element == 1)
        {
            GameObject tempCard = GameObject.Find("EarthPanel");
            if (InitialData.earthCardNum == 0)
            {
                Color tempColor = tempCard.transform.GetChild(1).GetComponent<Image>().color;
                tempCard.transform.GetChild(1).GetComponent<Image>().color=new Color(tempColor.r,tempColor.g,tempColor.b, 1.0f);
                tempCard.transform.GetChild(3).gameObject.SetActive(true);
            }
            InitialData.earthCardNum++;
            GameObject.Find("EarthPanel").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.earthCardNum.ToString();
            Debug.Log("EarthCardNum:"+InitialData.earthCardNum);
        }
        else if (elementCard.element == 2)
        {
            GameObject tempCard = GameObject.Find("FirePanel");
            if (InitialData.fireCardNum == 0)
            {
                Color tempColor = tempCard.transform.GetChild(1).GetComponent<Image>().color;
                tempCard.transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                tempCard.transform.GetChild(3).gameObject.SetActive(true);
            }
            InitialData.fireCardNum++;
            GameObject.Find("FirePanel").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.fireCardNum.ToString();
            Debug.Log("FireCardNum:" + InitialData.fireCardNum);
        }
        else if (elementCard.element == 3)
        {
            GameObject tempCard = GameObject.Find("MetalPanel");
            if (InitialData.metalCardNum == 0)
            {
                Color tempColor = tempCard.transform.GetChild(1).GetComponent<Image>().color;
                tempCard.transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                tempCard.transform.GetChild(3).gameObject.SetActive(true);
            }
            InitialData.metalCardNum++;
            GameObject.Find("MetalPanel").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.metalCardNum.ToString();
            Debug.Log("MetalCardNum:" + InitialData.metalCardNum);
        }
        else if (elementCard.element == 4)
        {
            GameObject tempCard = GameObject.Find("WaterPanel");
            if (InitialData.waterCardNum == 0)
            {
                Color tempColor = tempCard.transform.GetChild(1).GetComponent<Image>().color;
                tempCard.transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                tempCard.transform.GetChild(3).gameObject.SetActive(true);
            }
            InitialData.waterCardNum++;
            GameObject.Find("WaterPanel").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.waterCardNum.ToString();
            Debug.Log("WaterCardNum:" + InitialData.waterCardNum);
        }
        else if (elementCard.element == 5)
        {
            GameObject tempCard = GameObject.Find("WoodPanel");
            if (InitialData.woodCardNum == 0)
            {
                Color tempColor = tempCard.transform.GetChild(1).GetComponent<Image>().color;
                tempCard.transform.GetChild(1).GetComponent<Image>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 1.0f);
                tempCard.transform.GetChild(3).gameObject.SetActive(true);
            }
            InitialData.woodCardNum++;
            GameObject.Find("WoodPanel").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = InitialData.woodCardNum.ToString();
            Debug.Log("WoodCardNum:" + InitialData.woodCardNum);
        }
        Destroy(myHit.collider.gameObject);
    }
}
