using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RayCasterTest : MonoBehaviour {
    GraphicRaycaster test;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject temp = GameObject.Find("TSCanvas");
        if (temp != null)
        {
            test = temp.GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            test.Raycast(ped, results);
            Debug.Log(results.Count);
        }
	}
}
