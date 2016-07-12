using UnityEngine;
using System.Collections;

public class ElementCard : MonoBehaviour {
    private float distance;
    private Vector3 dir;
    private float speed;
    public int element;
	// Use this for initialization
	void Start () {
        distance = 0f;
        dir = new Vector3(0, -1, 0);
        speed = 1.6f;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
